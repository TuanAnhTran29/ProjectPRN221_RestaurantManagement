namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public interface IEmailSender
    {
        public int generateAuthenticationCode();
        public Task sendEmailAsync(String email, String subject, String message);
        public bool isAuthenticated(int code, int input);
    }
}
