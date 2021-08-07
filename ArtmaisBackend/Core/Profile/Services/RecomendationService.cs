using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Profile.Services
{
    public class RecomendationService : IHostedService, IDisposable
    {
        public RecomendationService(IAsyncProfileAccessRepository asyncProfileAccessRepository)
        {
            _mlContext = new MLContext();
            _asyncProfileAccessRepository = asyncProfileAccessRepository;
        }

        private readonly MLContext _mlContext;
        private readonly IAsyncProfileAccessRepository _asyncProfileAccessRepository;
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public void DoWork(object? state)
        {
            var categoryRating = _asyncProfileAccessRepository.GetAllCategoryRating();
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(categoryRating);
            var testDataView = _mlContext.Data.LoadFromEnumerable(categoryRating);

            var estimator = _mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "VisitorUserIdEnconded", inputColumnName: "VisitorUserId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "VisitedSubcategoryIdEnconded", inputColumnName: "VisitedSubcategoryId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdVisitanteEncoded",
                MatrixRowIndexColumnName = "subcategoriaDonoEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 200,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));
            var model = trainerEstimator.Fit(trainingDataView);

            var prediction = model.Transform(testDataView);
            _mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<CategoryRating, CategoryRatingPrediction>(model);
            var testInput = new CategoryRating { VisitorUserId = 10, VisitedSubcategoryId = 1 };
            var categoryRatingPrediction = predictionEngine.Predict(testInput);

            if (Math.Round(categoryRatingPrediction.Score, 1) > 1)
            {

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
