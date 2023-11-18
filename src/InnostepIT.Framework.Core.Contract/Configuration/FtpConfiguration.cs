namespace InnostepIT.Framework.Core.Contract.Configuration;

public class FtpConfiguration
{
    public string FtpUser { get; set; }
    public string FtpServer { get; set; }
    public int FtpPort { get; set; }
    public string FtpPassword { get; set; }
    public bool TestmodeActive { get; set; }
}