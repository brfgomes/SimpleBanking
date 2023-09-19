using SimpleBanking.Aplication;

namespace SimpleBanking.Infra.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<GenericResponse> SendEmail(string email);
    }
}
