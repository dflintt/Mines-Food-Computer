using Windows.ApplicationModel.Email;
using LightBuzz.SMTP;

namespace MessagingManager
{
    public sealed class EMailManager
    {
        public async void SendEmail(string subject, string body, string recipient)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 465, true, "minesfoodcomputer@gmail.com", "foodcomputed"))
            {
                EmailMessage emailMessage = new EmailMessage
                {
                    Subject = subject,
                    Body = body
                };
                emailMessage.To.Add(new EmailRecipient(recipient));
                emailMessage.Importance = EmailImportance.High;

                await client.SendMail(emailMessage);
            }

        }
    }
}