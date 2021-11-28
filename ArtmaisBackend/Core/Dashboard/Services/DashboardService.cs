using ArtmaisBackend.Core.Dashboard.Responses;
using ArtmaisBackend.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Dashboard.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _client;
        private readonly DbServiceConfiguration _dbServiceConfiguration;

        public DashboardService(IHttpClientFactory clientFactory, IOptions<DbServiceConfiguration> options)
        {
            _client = clientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(clientFactory));
            _dbServiceConfiguration = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<DashboardResponse> GetAsync(long userId)
        {
            var response = await _client.GetAsync($"{_dbServiceConfiguration.Url}dashboard/{userId}");

            var body = await response.Content.ReadAsStringAsync();

            var dashboardResponse = JsonConvert.DeserializeObject<DashboardResponse>(body);

            return dashboardResponse;
        }
    }
}
