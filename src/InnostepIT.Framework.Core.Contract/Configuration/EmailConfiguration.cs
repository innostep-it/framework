namespace InnostepIT.Framework.Core.Contract.Configuration;

public class EmailConfiguration
{
    public string ApiKey { get; set; }
    public string EmailSenderEmail { get; set; }
    public string EmailSenderName { get; set; }
    public string EmailReplyToEmail { get; set; }
    public string EmailReplyToName { get; set; }
    public string EmailTemplatesPath { get; set; }
    public AdditionalEmailHeader[] AdditionalEmailHeaders { get; set; }
}