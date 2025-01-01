using Infrastructure.Helper;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class DashboardService : IDashboardService
    {
        public async Task<HttpResponseMessage> GetDashboardDetails()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Dashboard/GetDashboardDetails");

            return response;
        }
    }
}
