using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class SessionInfoDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
