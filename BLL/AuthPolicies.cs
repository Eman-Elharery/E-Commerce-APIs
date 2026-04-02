namespace CompanySystem.BLL
{
   
    public static class AuthPolicies
    {
        public const string AdminOnly = "AdminOnly";

        public const string AuthenticatedUser = "AuthenticatedUser";

        public const string AdminOrUser = "AdminOrUser";
    }
}
