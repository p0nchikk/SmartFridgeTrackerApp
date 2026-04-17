using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models.DTOs
{
    public class RecipeResponseDto
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("availableIngredients")] public List<IngredientDto> AvailableIngredients { get; set; }
        [JsonPropertyName("missingIngredients")] public List<string> MissingIngredients { get; set; }
        [JsonPropertyName("instructions")] public List<string> Instructions { get; set; }
        [JsonPropertyName("type")] public int Type { get; set; }
        [JsonPropertyName("time")] public int Time { get; set; }
    }

    public class IngredientDto
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("amount_used")] public int AmountUsed { get; set; }
        [JsonPropertyName("display_text")] public string DisplayText { get; set; }
    }
}
