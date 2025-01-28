using Quartz;
using System.Net.Mail;
using System.Net;

namespace lr_fifteen.DB
{
    public class DBEmailSender: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (MailMessage mailMessage = new MailMessage("lrThirteenSerilog@gmail.com", "blinkotukpomerplak@gmail.com"))
            {
                mailMessage.Subject = "Database Update";
                mailMessage.Body = "New row added to db";
                using (SmtpClient smtpClient = new SmtpClient
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
