namespace SimpleBanking.Infra.Services;

public class AuthService
{
    public static async Task<bool> Execute()
    {
        try
        {   
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://run.mocky.io/v3/8fafdd68-a090-496f-8c9a-3442cf30dae6");

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
