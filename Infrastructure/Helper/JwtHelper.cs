using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Helper
{
    public static class JwtHelper
    {
        public static string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Extract the UserID claim
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID");
            return userIdClaim?.Value;
        }
    }

}
