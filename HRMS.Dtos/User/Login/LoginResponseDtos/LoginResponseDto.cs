namespace HRMS.Dtos.User.Login.LoginResponseDtos
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
