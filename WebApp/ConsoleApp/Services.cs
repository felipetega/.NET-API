using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class ApiService
    {
        private HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string apiUrl)
        {
            return await client.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> PostAsync(string apiUrl, City newCity)
        {
            string jsonBody = JsonSerializer.Serialize(newCity);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            return await client.PostAsync($"{apiUrl}/create", content);
        }

        public async Task<HttpResponseMessage> PutAsync(string apiUrl, int id, City updatedCity)
        {
            string jsonBody = JsonSerializer.Serialize(updatedCity);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            return await client.PutAsync($"{apiUrl}/update/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string apiUrl, int id)
        {
            return await client.DeleteAsync($"{apiUrl}/delete/{id}");
        }
    }

    
}
