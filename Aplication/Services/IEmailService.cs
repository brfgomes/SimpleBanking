namespace SimpleBanking.Aplication.Services
{
    public interface IEmailService
    {
        public Task<GenericResponse> SendEmail(string email);
    }
}
