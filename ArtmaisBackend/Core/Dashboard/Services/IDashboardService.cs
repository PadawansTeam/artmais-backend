using ArtmaisBackend.Core.Dashboard.Responses;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Dashboard.Services
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetAsync(long userId);
    }
}
