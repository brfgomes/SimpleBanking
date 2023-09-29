using SimpleBanking.Aplication.Services;

namespace SimpleBanking.Infra.Services;

public class MockAuthAdapter : IAuthenticationService
{
    public async Task<bool> Authenticate()
    {
        try
        {   
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://run.Mocky.io/v3/be0c6499-aaa9-4d58-88fd-f960b5989617");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
