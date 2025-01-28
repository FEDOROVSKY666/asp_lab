using Quartz;
using System.Net;
using System.Net.Mail;

namespace lr_fifteen.Services
{
    public class EmailSender : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (MailMessage mailMessage = new MailMessage("lrThirteenSerilog@gmail.com", "blinkotukpomerplak@gmail.com"))
            {
                mailMessage.Subject = "Some email";
                mailMessage.Body = "Some body";
                using(SmtpClient smtpClient = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("lrThirteenSerilog@gmail.com", "gtqf rtqb cwwc hnwc")
                })
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
