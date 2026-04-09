using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class CookBook
    {
        public List<Recipe> RecipesList { get; set; }

        public CookBook()
        {
            RecipesList = new List<Recipe>();
        }

    }
}
