using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class CookBook
    {
        public List<Recipe> FavoritesList { get; set; }
        public List<Recipe> RecipesList { get; set; }

        public CookBook()
        {
            FavoritesList = new List<Recipe>();
            RecipesList = new List<Recipe>();
        }

    }
}
