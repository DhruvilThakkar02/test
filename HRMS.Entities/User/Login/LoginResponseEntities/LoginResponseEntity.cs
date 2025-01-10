namespace HRMS.Entities.User.Login.LoginResponseEntities
{
    public class LoginResponseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public TokenInformation? TokenDetails { get; set; }   
    }
    public class TokenInformation
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }
}
