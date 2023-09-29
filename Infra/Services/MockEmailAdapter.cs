using SimpleBanking.Aplication;
using System.Text;
using SimpleBanking.Aplication.Services;

namespace SimpleBanking.Infra.Services;

public class MockEmailAdapter : IEmailService
{
    public async Task<GenericResponse> SendEmail(string email)
    {
        email = "bruno.gomes@fourlions.com.br";

        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //url apenas para testes, ela não é para essa finalidade de email

                var jsonContent = $"{{\"email\": \"{email}\"}}";
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(
                    "https://8kw7y.wireMockapi.cloud/sendnotification", content);

                var responseMessage = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return new GenericResponse(true, responseMessage);
                }
                else
                {
                    return new GenericResponse(false, responseMessage);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
