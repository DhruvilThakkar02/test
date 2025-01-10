namespace HRMS.Dtos.User.Login.LoginResponseDtos
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public TokenInfo? TokenDetails { get; set; }       
    } 
    public class TokenInfo
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }
   
}
