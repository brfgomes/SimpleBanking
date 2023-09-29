using SimpleBanking.Aplication.Services;

namespace SimpleBanking.Aplication.Factories;

public interface IServicesFactory
{
    IAuthenticationService CreateAuthenticationService();
    IEmailService CreateEmailService();
}