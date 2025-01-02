using Infrastructure.Helper;
using Service.Interfaces;

namespace Service.Implementations
{
    public class DashboardService : IDashboardService
    {
        public async Task<HttpResponseMessage> GetDashboardDetails()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "dashboard");

            return response;
        }
    }
}
