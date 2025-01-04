using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userName, DateTime expiration);
        bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal);
        IEnumerable<Claim> GetClaims(string token);
    }
}
