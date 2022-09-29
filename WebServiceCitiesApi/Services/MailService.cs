namespace WebServiceCitiesApi.Services
{

    //this must be added in builder.services
    //gör om denna till ett interface
    public class MailService : IMailService
    {
        //läsa värde från appsettingsfilerna appsettings.json eller appsettings.Development.json,appsettings.Production.json
        //detta med configurationsfilen ingår redan i ramverket, behövs inte sättas explicit i Program.cs
        //värde läses från båda filerna, om samma nyckel finns då vinner sista
        public MailService(IConfiguration config)
        {
            mailfrom = config["MailSettings:mailToAddress"];
            mailfrom = config["MailSettings:mailFromAddress"];

            /* så här ser det ut i den filen, case-insensitive
               "MailSettings": {
                "mailToAddress": "mymailtoadress@mail.com",
                 "mailFromAddress":  "mymailfromadress@mail.com"
                    },
             
             */
        }

        private readonly string mailfrom = "bbb";
        private readonly string mailto = "bbb";

   

        public bool SendMail(string subject, string message) { return true; }
    }


    public class CloudMailService : IMailService
    {
        private string from = "bbb";
        private string to = "bbb";

        public bool SendMail(string subject, string message) { return true; }
    }
}
