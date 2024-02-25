using Microsoft.Extensions.Configuration;
using MyApp.Models;

namespace MyApp.Services
{
    public interface ISynonymsService
    {
        Task<HttpResponseMessage> GetSynonymsAsync(string word);
    }
    public class SynonymsService : ISynonymsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public SynonymsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<HttpResponseMessage> GetSynonymsAsync(string word)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://api.api-ninjas.com/v1/thesaurus?word={word}&X-Api-Key={_configuration.GetSection("ApiKey").Value}");
            await Console.Out.WriteLineAsync(word);
            return response;
        }
    }
}
