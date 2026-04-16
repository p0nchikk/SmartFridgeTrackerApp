using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Recipe
    {
        public enum RecipeType
        {
            Breakfast = 1,
            Lunch = 2,
            Dinner = 3
        }

        #region Variables Declaration
        public string Id { get; set; } //property for firebase
        public string? Name { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty; // Keep short for UI cards
        public List<string> Instructions { get; set; } = new List<string>(); // Step-by-step guide

        //List of products needed for cooking that are currently in the fridge
        public List<Product> AvailableIngredients { get; set; } =  new List<Product>();

        // List of missing ingredients (salt, spices, etc.) not tracked in the fridge
        public List<string> MissingIngredients { get; set; } = new List<string>();
        public RecipeType? Type { get; set; }
        public int CookingTime { get; set; } //in minutes
        #endregion

        #region Constructors
        public Recipe() { } //Parameterless constructor for Firebase deserialization


        //Constructor without id, for new products that haven't been saved to Firebase yet
        public Recipe(
             string name,
             string description,
             List<string> instructions, // Added instructions
             List<string> listWithID,
             List<string> missingIngredients,
             int typeNum,
             int cookingTime) // Added cooking time
        {
            Id = Guid.NewGuid().ToString(); // Generate a unique ID for Firebase
            Name = name;
            Description = description;
            Instructions = instructions ?? new List<string>();

            AvailableIngredients = new List<Product>();

            // Look up products in the fridge by ID
            if (listWithID != null)
            {
                var fridgeProducts = AppService.GetInstance().loggedInUser.Fridge.ProductsList;
                foreach (var productID in listWithID)
                {
                    var product = fridgeProducts.FirstOrDefault(item => item.Id == productID);
                    if (product != null)
                    {
                        AvailableIngredients.Add(product);
                    }
                }
            }

            MissingIngredients = missingIngredients ?? new List<string>();

            // Cast the int to our Enum
            Type = (RecipeType)typeNum;
            CookingTime = cookingTime;
        }
        #endregion

        #region Functions 

        #endregion

    }
}
