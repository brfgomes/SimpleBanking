namespace SimpleBanking.Infra.Services;

public class EmailService
{
    public static async Task<bool> Execute()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://o4d9z.mocklab.io/notify");

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
