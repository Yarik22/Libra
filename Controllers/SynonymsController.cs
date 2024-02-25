using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Services;
using System.Net;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SynonymsController : ControllerBase
    {
        List<string> words = new List<string>
        {
            "beautiful", "happy", "exciting", "creative", "wonderful",
            "amazing", "fantastic", "awesome", "brilliant", "joyful",
            "lovely", "delightful", "fun", "inspiring", "peaceful"
        };
        private readonly ISynonymsService _synonymsService;
        public SynonymsController(ISynonymsService synonymsService)
        {
            _synonymsService = synonymsService;
        }

        static string GetRandomWord(List<string> list)
        {
            Random rand = new Random();
            int randomIndex = rand.Next(list.Count);
            return list[randomIndex];
        }

        [HttpGet(Name = "SynonymsOfRandomWord")]
        public async Task<SynonymsResponse> Get()
        {
            SynonymsResponse response = new();
            var word = GetRandomWord(words);
            HttpResponseMessage retrievedData = await _synonymsService.GetSynonymsAsync(word);
            var retrievedSynonyms = await retrievedData.Content.ReadFromJsonAsync<SynonymsData>();
            response.StatusCode = retrievedData.StatusCode;
            try
            {
                //throw new HttpRequestException();
                response.Message = $"Wrong retrivement of data. Word : {word}";
                retrievedData.EnsureSuccessStatusCode();
                if (retrievedData.IsSuccessStatusCode)
                {
                    response.Message = $"Successful retrivement of data. Word : {word}";
                    response.Data = retrievedSynonyms;
                    return response;
                }
                return response;
            }
            catch (HttpRequestException ex)
            {
                response.Message = $"Failed to retrieve data. Word : {word}";
                response.StatusCode = HttpStatusCode.InternalServerError;
                Console.WriteLine($"Error fetching data from API: {ex.Message}");
                return response;
            }
        }
        [HttpPost(Name = "SynonymsOfWord")]
        public async Task<SynonymsResponse> Post([FromQuery] string word)
        {
            SynonymsResponse response = new();
            HttpResponseMessage retrievedData = await _synonymsService.GetSynonymsAsync(word);
            var retrievedSynonyms = await retrievedData.Content.ReadFromJsonAsync<SynonymsData>();
            response.StatusCode = retrievedData.StatusCode;
            try
            {
                response.Message = $"Wrong retrivement of data. Word : {word}";
                retrievedData.EnsureSuccessStatusCode();
                if (retrievedData.IsSuccessStatusCode)
                {
                    response.Message = $"Successful retrivement of data. Word : {word}";
                    response.Data = retrievedSynonyms;
                    return response;
                }
                return response;
            }
            catch (HttpRequestException ex)
            {
                response.Message = $"Failed to retrieve data. Word : {word}";
                response.StatusCode = HttpStatusCode.InternalServerError;
                Console.WriteLine($"Error fetching data from API: {ex.Message}");
                BadRequest("The request body cannot be null.");
                return response;
            }
        }
    }
}
