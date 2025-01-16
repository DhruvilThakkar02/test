namespace HRMS.Utility.Helpers.Passwords
{
    public static class PasswordHashingUtility
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
        public static bool Verify(string password, string storedHash)
        {
            try
            {
                var res = BCrypt.Net.BCrypt.EnhancedVerify(password, storedHash);
                return res;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error verifying password.", ex);
            }
        }
    }
}
