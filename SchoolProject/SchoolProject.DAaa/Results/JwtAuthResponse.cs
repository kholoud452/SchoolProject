using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Results
{
    public class JwtAuthResponse
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
    public class RefreshToken
    {
        public string TokenToRefresh { get; set; }
        public string UserName { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
