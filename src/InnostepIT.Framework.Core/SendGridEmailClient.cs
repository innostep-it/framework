using InnostepIT.Framework.Core.Contract;
using InnostepIT.Framework.Core.Contract.Configuration;
using InnostepIT.Framework.Core.Contract.Exceptions;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace InnostepIT.Framework.Core;

public class SendGridEmailClient : IEmailClient
{
    private readonly EmailConfiguration _configuration;
    private readonly ILogger<SendGridEmailClient> _logger;

    public SendGridEmailClient(ILogger<SendGridEmailClient> logger, EmailConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendAsync(string subject, string toSenderEmail, string toSenderName, string plainTextContent = "",
        string htmlContent = "")
    {
        var client = new SendGridClient(_configuration.ApiKey);

        var from = new EmailAddress(_configuration.EmailSenderEmail, _configuration.EmailSenderName);
        var replyTo = new EmailAddress(_configuration.EmailReplyToEmail, _configuration.EmailReplyToName);
        var to = new EmailAddress(toSenderEmail, toSenderName);

        var email = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        email.ReplyTo = replyTo;

        var additionalHeaders = _configuration.AdditionalEmailHeaders;
        if (additionalHeaders?.Length > 0)
        {
            _logger.LogDebug(
                $"{additionalHeaders.Length} additional email headers are configured and will be applied to the email.");
            foreach (var additionalHeader in additionalHeaders)
                email.AddHeader(additionalHeader.HeaderName, additionalHeader.HeaderValue);
        }

        var response = await client.SendEmailAsync(email);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.DeserializeResponseBodyAsync();
            var errorMessages = string.Join(",", errorBody.Select(e => e.Value));
            var errorMessage =
                $"{typeof(SendGridEmailClient)}: Failed to send email to {toSenderEmail} because of following errors: {errorMessages}";
            _logger.LogError(errorMessage);
            throw new EmailDeliveryException(errorMessage);
        }
    }
}