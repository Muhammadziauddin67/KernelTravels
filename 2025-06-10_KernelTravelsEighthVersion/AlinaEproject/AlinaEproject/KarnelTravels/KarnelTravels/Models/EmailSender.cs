using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
 
    namespace KarnelTravels.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync (string email, string subject, string htmlMessage) 
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("rashiduzma686@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };


            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Use App Password if 2FA is enabled
                emailClient.Authenticate("rashiduzma686@gmail.com", "cmcg ctty lnus mciz\r\n");

                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}
