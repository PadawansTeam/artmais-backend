using ArtmaisBackend.Core.Recomendation.Responses;
using ArtmaisBackend.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Recomendation.Services
{
    public class RecomendationService : IRecomendationService
    {
        private readonly HttpClient _client;
        private readonly DbServiceConfiguration _dbServiceConfiguration;

        public RecomendationService(IHttpClientFactory clientFactory, IOptions<DbServiceConfiguration> options)
        {
            _client = clientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(clientFactory));
            _dbServiceConfiguration = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<RecomendationResponse> GetAsync(int subcategory)
        {
            var response = await _client.GetAsync($"{_dbServiceConfiguration.Url}recomendation/{subcategory}");

            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<RecomendationResponse>(body);
        }
    }
}
