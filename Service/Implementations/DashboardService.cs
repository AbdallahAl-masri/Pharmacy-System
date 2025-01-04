using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;
using System.Net.Http.Headers;

namespace Service.Implementations
{
    public class DashboardService : IDashboardService
    {
        public async Task<HttpResponseMessage> GetDashboardDetails(string token)
        {

            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "dashboard");

            return response;
        }
    }
}
