namespace HRMS.Utility.Helpers.Passwords
{
    public static class PasswordHashingUtility
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, storedHash);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error verifying password.", ex);
            }
        }
    }
}
