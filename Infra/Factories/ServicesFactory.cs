using SimpleBanking.Aplication.Factories;
using SimpleBanking.Aplication.Services;
using SimpleBanking.Infra.Services;

namespace SimpleBanking.Infra.Factories;

public class ServicesFactory : IServicesFactory
{
    public IAuthenticationService CreateAuthenticationService()
    {
        return new MockAuthAdapter();
    }

    public IEmailService CreateEmailService()
    {
        return new MockEmailAdapter();
    }
}