namespace SimpleBanking.Infra.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<bool> Authenticate();
    }
}
