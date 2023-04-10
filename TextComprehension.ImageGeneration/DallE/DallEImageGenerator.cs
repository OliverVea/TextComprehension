using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TextComprehension.ImageGeneration.Interface;
using TextComprehension.ImageGeneration.Models;

namespace TextComprehension.ImageGeneration.DallE
{
    public class DallEImageGenerator : IImageGenerator
    {
        private readonly string _apiKey;

        public DallEImageGenerator(string apiKey)
        {
            _apiKey = apiKey;
        }
        
        public async Task<ImageGenerationResponse> GenerateImageAsync(ImageGenerationRequest request)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            
            var vals = new[]
            {
                "\"prompt\": " + request.Prompt + "\"",
                "\"n\": 1",
                "\"size\": \"1024x1024\""
            };
            
            var serializedContent = "{" + string.Join(", ", vals) + "}";
            var stringContent = new StringContent(serializedContent, System.Text.Encoding.UTF8, "application/json");
            var uri = new System.Uri("https://api.openai.com/v1/images/generations");

            var response = await client.PostAsync(uri, stringContent);

            return new ImageGenerationResponse();
        }
    }
}