using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Teams.API.Services
{
    public class PeopleAPIService
    {

        public static async Task UpdateStatus(string id)
        {
            HttpClient httpClient = new();

            try
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri("https://localhost:44383/");
                
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PutAsync($"api/People/{id}/Status", null);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

    }
}
