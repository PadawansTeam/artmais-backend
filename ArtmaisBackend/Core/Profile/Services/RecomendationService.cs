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
        public RecomendationService(IAsyncInterestRepository asyncInterestRepository, IAsyncProfileAccessRepository asyncProfileAccessRepository)
        {
            _mlContext = new MLContext();
            _asyncInterestRepository = asyncInterestRepository;
            _asyncProfileAccessRepository = asyncProfileAccessRepository;
        }

        private readonly MLContext _mlContext;
        private readonly IAsyncInterestRepository _asyncInterestRepository;
        private readonly IAsyncProfileAccessRepository _asyncProfileAccessRepository;
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));
            return Task.CompletedTask;
        }

        public void DoWork(object? state)
        {
            var categoriesRating = _asyncProfileAccessRepository.GetAllCategoryRating();
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(categoriesRating);
            var testDataView = _mlContext.Data.LoadFromEnumerable(categoriesRating);

            var estimator = _mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "VisitorUserIdEnconded", inputColumnName: "VisitorUserId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "VisitedSubcategoryIdEnconded", inputColumnName: "VisitedSubcategoryId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "VisitorUserIdEnconded",
                MatrixRowIndexColumnName = "VisitedSubcategoryIdEnconded",
                LabelColumnName = "VisitNumber",
                NumberOfIterations = categoriesRating.Count(),
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));
            var model = trainerEstimator.Fit(trainingDataView);

            var prediction = model.Transform(testDataView);
            _mlContext.Regression.Evaluate(prediction, labelColumnName: "VisitNumber", scoreColumnName: "Score");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<CategoryRating, CategoryRatingPrediction>(model);
            
            foreach(var categoryRating in categoriesRating)
            {
                if (Math.Round(predictionEngine.Predict(categoryRating).Score, 1) >= 2)
                {
                    if (!_asyncInterestRepository.GetInterestByUserIdAndSubcategoryId(categoryRating))
                        _asyncInterestRepository.Create(categoryRating);
                }
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
