using DotNetEnv;
using Google.GenAI;
using Google.GenAI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartFridgeTracker.Services
{
    public class GeminiAIService : IAIService
    {
        private string modelName = "gemini-3-flash";
        private Client _client;
        private GenerateContentConfig _config;
        public GeminiAIService()
        {
            //Load the API key from the .env file
            Env.Load(".env");
            var apiKey = System.Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            //Initialize the Gemini API client
            _client = new Client(apiKey: apiKey);

            //Set configuration
            _config = new GenerateContentConfig
            {
                ResponseMimeType = "application/json"
            };
        }

        public async Task<string> GetRecipeAsync(string ingredients)
        {
            throw new NotImplementedException();
        }

        public async Task<string> IdentifyProductAsync(byte[] imageBytes)
        {
            throw new NotImplementedException();
        }
    }
}
