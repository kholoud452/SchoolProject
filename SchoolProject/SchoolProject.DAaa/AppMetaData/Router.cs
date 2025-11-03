namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string singleRouter = "{id}";
        public const string root = "API/V1/";
        public static class StudentRouting
        {
            public const string Prefix = root + "Student/";
            public const string List = Prefix + "List";
            public const string GetByID = Prefix + singleRouter;
            public const string Create = Prefix + "Create";
            public const string CreateStudentSubject = Prefix + "CreateStudentSubject";
            public const string Edit = Prefix + "Edit";
            public const string Pagination = Prefix + "Pagination";
            public const string Delete = Prefix + singleRouter;
        }
        public static class DepartmentRouting
        {
            public const string Prefix = root + "Department/";
            public const string GetByID = Prefix + "ID";
            public const string GetAll = Prefix + "GetAll";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + singleRouter;
        }
        public static class AuthenticationRouting
        {
            public const string Prefix = root + "Authentication/";
            public const string Login = Prefix + "Login";
            public const string RefreshToken = Prefix + "Refresh-Token";
            public const string ValidatorToken = Prefix + "Validator-Token";
            public const string ConfirmResetPassword = Prefix + "Confirm-Reset-Password";
            public const string ResetNewPassword = Prefix + "Reset-New-Password";
            public const string SendResetPasswordCode = Prefix + "Send-Reset-Password";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
        }
        public static class AuthorizationRouting
        {
            public const string Prefix = root + "Authorization/";
            public const string GetAll = Prefix + "Role/List";
            public const string AddRole = Prefix + "Role/Create";
            public const string EditRole = Prefix + "Role/Edit";
            public const string UpdateUserRoles = Prefix + "Role/Update-User-Roles";
            public const string UpdateUserClaims = Prefix + "Claims/Update-User-Claims";
            public const string DeleteRole = Prefix + "Role/Delete/{id}";
            public const string GetRoleById = Prefix + "Role/Get-By-ID/{id}";
            public const string ManageUserRoles = Prefix + "Role/Manage-User-Roles/{id}";
            public const string ManageUserClaims = Prefix + "Claims/Manage-User-Claims/{id}";
        }
        public static class AccountRouting
        {
            public const string Prefix = root + "Account/";
            public const string Register = Prefix + "Register";
            public const string Pagination = Prefix + "Pagination";
            public const string GetByID = Prefix + singleRouter;
            public const string Edit = Prefix + "Edit";
            public const string ChangePassword = Prefix + "Change-Password";
            public const string Delete = Prefix + singleRouter;
        }
        public static class EmailRouter
        {
            public const string Prefix = root + "Email/";
            public const string SendEmail = Prefix + "Send-Email";
        }
        public static class SubjectRouting
        {
            public const string Prefix = root + "Subject/";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string GetAll = Prefix + "GetAll";
            public const string GetByID = Prefix + singleRouter;
            public const string Delete = Prefix + singleRouter;

        }
    }
}
