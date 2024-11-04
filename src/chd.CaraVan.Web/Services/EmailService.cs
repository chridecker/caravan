using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using chd.CaraVan.Devices.Contracts.Interfaces;
using chd.CaraVan.Contracts.Settings;

namespace chd.CaraVan.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptionsMonitor<EmailSettings> _optionsMonitor;

        public EmailService(IOptionsMonitor<EmailSettings> optionsMonitor, ILogger<EmailService> logger)
        {
            this._optionsMonitor = optionsMonitor;
        }
        public async Task SendEmail(string caption, string body, CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = new SmtpClient(this._optionsMonitor.CurrentValue.Smtp, this._optionsMonitor.CurrentValue.Port)
                {
                    Credentials = new NetworkCredential(this._optionsMonitor.CurrentValue.Username, this._optionsMonitor.CurrentValue.Password),
                    EnableSsl = true,
                };

                var mail = new MailMessage(this._optionsMonitor.CurrentValue.From, this._optionsMonitor.CurrentValue.To)
                {
                    From = new MailAddress(this._optionsMonitor.CurrentValue.From, this._optionsMonitor.CurrentValue.FromName),
                    IsBodyHtml = false,
                    Body = body,
                    Subject = caption,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8
                };
                await client.SendMailAsync(mail, cancellationToken);
            }
            catch { }
        }
    }
}
