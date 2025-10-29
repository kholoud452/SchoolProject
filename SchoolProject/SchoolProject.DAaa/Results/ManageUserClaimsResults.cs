namespace SchoolProject.Data.Results
{
    public class ManageUserClaimsResults
    {
        public string UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
