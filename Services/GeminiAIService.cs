using DotNetEnv;
using Google.GenAI;
using Google.GenAI.Types;
using SmartFridgeTracker.Models;
using SmartFridgeTracker.Models.DTOs;
using SmartFridgeTracker.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace SmartFridgeTracker.Services
{
    public class GeminiAIService : IAIService
    {
        static private string _modelName = "gemini-3-flash";
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
            //Check if list is empty
            if (availableProducts == null || availableProducts.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No products available for recipe generation.");
                return null;
            }

            //Prepare Data
            Schema recipeSchema = GetRecipeResponseSchema();
            string jsonProducts = RecipePromptBuilder.GetSimplifiedProductList(availableProducts);
            string prompt = RecipePromptBuilder.BuildChefPrompt(jsonProducts);

            try
            {
                //Call the API 
                var response = await _client.Models.GenerateContentAsync(
                    _modelName,
                    prompt,
                    _config
                );

                //Extract the text and Map to Recipe model
                string jsonResponse = response.Text;
                return MapJsonToRecipe(jsonResponse);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Gemini Error: {ex.Message}");
                return null;
            }
        }

        private Recipe MapJsonToRecipe(string jsonResponse)
        {
            var dto = JsonSerializer.Deserialize<RecipeResponseDto>(jsonResponse);

            // Extract the IDs from the DTO to pass to our Recipe constructor
            List<string> ingredientIds = dto.AvailableIngredients.Select(i => i.Id).ToList();

            return new Recipe(
                dto.Name,
                dto.Description,
                dto.Instructions,
                ingredientIds,
                dto.MissingIngredients,
                dto.Type,
                dto.Time
            );
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
                        "availableIngredients", new Schema 
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
                Required = new List<string> { "name", "description", "availableIngredients", "missingIngredients", "instructions", "type", "time" }
            };
        }
        #endregion

        public async Task<string> IdentifyProductAsync(byte[] imageBytes)
        {
            throw new NotImplementedException();
        }
    }
}
