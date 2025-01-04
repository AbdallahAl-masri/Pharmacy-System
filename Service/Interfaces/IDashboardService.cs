using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IDashboardService
    {
        public Task<HttpResponseMessage> GetDashboardDetails(string token);
    }
}
