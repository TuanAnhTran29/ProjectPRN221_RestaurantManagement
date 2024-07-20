using System.Net.Mail;
using System.Net;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class EmailSender: IEmailSender
    {
        public int generateAuthenticationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public async Task sendEmailAsync(String email, String subject, String message)
        {
            var mail = "tuananhtran291003@gmail.com";
            var password = "bvqquguldrnovxyx";

            using (var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true // This enables STARTTLS
            })
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mail, "PosDash"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }

        public bool isAuthenticated(int code, int input)
        {
            if(code == input)
            {
                return true;
            }
            return false;
        }
    }
}
