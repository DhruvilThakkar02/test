namespace HRMS.Dtos.User.Login.LoginRequestDtos
{
    public class LoginRequestDto
    {
        public string SubdomainName { get; set; } = string.Empty;
        public string UserNameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
