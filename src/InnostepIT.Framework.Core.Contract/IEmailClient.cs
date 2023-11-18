namespace InnostepIT.Framework.Core.Contract;

public interface IEmailClient
{
    Task SendAsync(string subject, string toSenderEmail, string toSenderName, string plainTextContent = "",
        string htmlContent = "");
}