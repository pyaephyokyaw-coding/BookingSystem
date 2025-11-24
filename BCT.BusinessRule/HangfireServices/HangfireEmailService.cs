using System.Net.Mail;
using System.Net;

namespace BCT.BusinessRule.Services.HangfireServices;

public class HangfireEmailService
{
    private readonly string email = "pyaephyokyaw.dev@gmail.com";
    private readonly string appPassword = "abc";

    public void SendEmail(string toEmail, string subject, string body)
    {
        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(email, appPassword),
            EnableSsl = true,
        };

        var mail = new MailMessage
        {
            From = new MailAddress(email),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mail.To.Add(toEmail);

        smtp.Send(mail);
    }
}
