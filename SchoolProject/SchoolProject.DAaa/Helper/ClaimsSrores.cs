using System.Security.Claims;

namespace SchoolProject.Data.Helper
{
    public static class ClaimsSrores
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create","false"  ),
            new Claim("Edit","false"  ),
            new Claim("Delete","false"  )
        };
    }
}
