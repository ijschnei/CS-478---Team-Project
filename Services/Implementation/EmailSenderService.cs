using Microsoft.AspNetCore.Identity.UI.Services;
using NuGet.Configuration;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace CS478_EventPlannerProject.Services.Implementation
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_configuration["EmailSettings:SmtpHost"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(_configuration["EmailSettings:SmtpUser"], _configuration["EmailSettings:SmtpPassword"]),
                EnableSsl = true
            };
            return client.SendMailAsync(
                new MailMessage(_configuration["EmailSettings:SenderEmail"], email, subject, htmlMessage) { IsBodyHtml = true });
        }
    }
}
