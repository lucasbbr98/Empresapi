namespace Validators
{
    public static class ValidatorMethods
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            if (password.Length < 6)
                return false;
            return true;
        }
    }
}
