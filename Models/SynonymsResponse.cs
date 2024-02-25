using System.Net;

namespace MyApp.Models
{
    public record SynonymsData(string Word, List<string> Synonyms, List<string> Antonyms);
    public class SynonymsResponse
    {
        public string? Message { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public SynonymsData? Data { get; set; }
    };
}
