namespace HRMS.Entities.User.Login.LoginRequestEntities
{
    public class LoginRequestEntity
    {
        public string SubdomainName { get; set; } = string.Empty;
        public string UserNameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
