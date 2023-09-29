namespace SimpleBanking.Aplication.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> Authenticate();
    }
}
