using DotNetEnv;
using Google.GenAI;
using Google.GenAI.Types;
using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace SmartFridgeTracker.Services
{
    public class GeminiAIService : IAIService
    {
        static private string modelName = "gemini-3-flash";
        private Client _client;
        private GenerateContentConfig _config;

        //Singleton Pattern
        static private GeminiAIService instance;
        static public GeminiAIService GetInstance()
        {
            if (instance == null)
            {
                instance = new GeminiAIService();
            }
            return instance;
        }

        //Constructor
        public GeminiAIService(){
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

        #region Recipe Generation
        public async Task<Recipe> GetRecipeAsync(List<Product> availableProducts)
        {
            // 1. Prepare Data
            Schema recipeSchema = GetRecipeResponseSchema();
            string jsonProducts = GetSimplifiedProductList(availableProducts);
            string prompt = GetChefPromt(jsonProducts);


        }

        private string GetChefPromt(string jsonProducts)
        {
            return $@"
                You are a professional chef. Suggest a delicious recipe using ONLY the products provided in the list below. 

                Available Products (JSON format):
                {jsonProducts}

                Rules:
                1. Use the 'id' from the provided list for the 'ingredients' field.
                2. If a recipe needs 'Kitchen Basics' that are not in the list (like salt, water, pepper, or oil), include them in the 'missing_ingredients' list.
                3. The 'description' must be a single, catchy sentence for a mobile UI card.
                4. The 'type' must be 1 (Breakfast), 2 (Lunch), or 3 (Dinner).
                5. Output MUST be in raw JSON format matching the provided schema.";
        }
        private string GetSimplifiedProductList(List<Product> availableProducts)
        {
            var simplifiedProducts = availableProducts.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                count = p.Count,
                quantity = p.Quantity
            }).ToList();

            string jsonProducts = JsonSerializer.Serialize(simplifiedProducts);
            return jsonProducts;
        }
        private Schema GetRecipeResponseSchema()
        {
            return new Schema
            {
                Title = "Recipe",
                Type = "object",
                Properties = new Dictionary<string, Schema>
                {
                    {
                        "name", new Schema { Type = "string", Title = "Name" }
                    },
                    {
                        "description", new Schema { Type = "string", Title = "Short Description in one or two sentences" }
                    },
                    {
                        "avaliableIngredients", new Schema 
                        {
                           Type = "array",
                                Items = new Schema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, Schema>
                                    {
                                        { "id", new Schema { Type = "string", Description = "The ID of the product provided in the input list" } },
                                        { "amount_used", new Schema { Type = "integer", Description = "How many units (count) used" } },
                                        { "display_text", new Schema { Type = "string", Description = "Full description e.g., '2 large eggs'" } }
                                    }
                                }                
                        }                      
                    },
                    {
                        "missingIngredients", new Schema
                        {
                            Type = "array",
                            Items = new Schema { Type = "string" },
                            Description = "List of basics like salt/spices not in the fridge"
                        }
                    },
                    {
                        "instructions", new Schema
                        {
                            Type = "array",
                            Items = new Schema { Type = "string" },
                            Description = "Step-by-step cooking instructions"
                        }
                    },
                    {
                        "type", new Schema { Type = "integer", Description = "1 - Breakfast, 2 - Lunch, 3 - Dinner", Title = "Type" }
                    },
                    {
                        "time", new Schema { Type = "integer" , Title = "Cooking Time", Description = "Cooking time in minutes" }
                    }
                },
                Required = new List<string> { "name", "description", "avaliableIngredients", "missingIngredients", "instructions", "type", "time" }
            };
        }
        #endregion

        public async Task<string> IdentifyProductAsync(byte[] imageBytes)
        {
            throw new NotImplementedException();
        }
    }
}
