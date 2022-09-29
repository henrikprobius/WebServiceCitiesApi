namespace WebServiceCitiesApi.Services
{
    public interface IMailService
    {
        bool SendMail(string subject, string message);
    }
}