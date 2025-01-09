using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Newtonsoft.Json;
using Repository.IRepository;
using Service.Interfaces;
using System.Collections.Concurrent;
using System.Text;

namespace Service.Implementations
{
    public class SessionService : ISessionService
    {

        public async Task<bool> IsSessionActiveAsync(SessionInfoDTO sessionDTO)
        {
            HttpClient client = new HttpClient();
            var context = new StringContent(JsonConvert.SerializeObject(sessionDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ConfigSettings.BaseApiUrl}session/active", context);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            return false;
        }

        public async Task RegisterSessionAsync(SessionInfoDTO sessionDTO)
        {
            HttpClient client = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(sessionDTO);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ConfigSettings.BaseApiUrl}session", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create or update the session.");
            }
        }

        public async Task InvalidateSessionAsync(SessionInfoDTO sessionDTO)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(sessionDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ConfigSettings.BaseApiUrl}session/invalidate", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete the session.");
            }
        }
    }


}
