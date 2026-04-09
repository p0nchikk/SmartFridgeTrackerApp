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
        #region Variables Declaration
        public enum Type { Breakfast, Lunch, Dinner }; //constant recipe types

        public string Id { get; set; } //property for firebase
        public string? Name { get; set; }
        public string? Description { get; set; }         
        public List<Product?>? ProductsList { get; set; } //List of products needed for cooking, think about type string or Product
        public Type? RecipeType { get; set; }
        #endregion

        #region Constructors
        public Recipe() { } //Parameterless constructor for Firebase deserialization


        //Constructor without id, for new products that haven't been saved to Firebase yet
        public Recipe(string name, string description, List<string> listWithID, int typeNum) //I have to tell to gemini recipe types and their key numbers
        {
            Id = String.Empty;
            Name = name;
            Description = description;
            ProductsList = new List<Product?>();
            foreach (var productID in listWithID) //Add products by their ID
            {
                var product = AppService.GetInstance().loggedInUser.Fridge.ProductsList.FirstOrDefault(item => item.Id == productID);
                ProductsList.Add(product);
            }
            RecipeType = (Type)typeNum;
        }

        //Constructor with id
        public Recipe(string id, string name, string description, List<string> listWithID, int typeNum) //I have to tell to gemini recipe types and their key numbers
        {
            Id = id;
            Name = name;
            Description = description;
            ProductsList = new List<Product?>();
            foreach (var productID in listWithID) //Add products by their ID
            {
                var product = AppService.GetInstance().loggedInUser.Fridge.ProductsList.FirstOrDefault(item => item.Id == productID);
                ProductsList.Add(product);
            }
            RecipeType = (Type)typeNum;
        }


        #endregion

        #region Functions 

        #endregion

    }
}
