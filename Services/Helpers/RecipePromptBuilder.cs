using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Services.Helpers
{
    public static class RecipePromptBuilder
    {
        public static string BuildChefPrompt(string jsonProducts)
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

        public static string GetSimplifiedProductList(List<Product> availableProducts)
        {
            var simplified = availableProducts.Select(p => new { id = p.Id, name = p.Name, quantity = p.Quantity });
            return JsonSerializer.Serialize(simplified);
        }
    }
}
