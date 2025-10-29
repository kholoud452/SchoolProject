namespace SchoolProject.Data.Results
{
    public class ManageUserRolesResults
    {

        public string UserId { get; set; }
        public List<Roles> Roles { get; set; }
    }
    public class Roles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}

