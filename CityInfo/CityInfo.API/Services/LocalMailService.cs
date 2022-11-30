namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            //TODO: this way we take the configurations from the appsetting.json file and use them
            this._mailTo = configuration["mailSettings:mailToAddress"];
            this._mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            // send mail output to console.window
            Console.WriteLine($"Mail from {this._mailFrom} to {this._mailTo} with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
//"mailFromAddress": "noreplay@mycompany.com"
//    "mailToAddress": "admin.mycompany.com",
//    mailSettings