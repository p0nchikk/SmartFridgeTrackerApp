using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Services
{
    public interface IAIService //Contract for AI services, such as Gemini API
    {
        Task<string> GetRecipeAsync(string ingredients); //Get recipe based on identified ingredients
        Task<string> IdentifyProductAsync(byte[] imageBytes); //Identify product based on image bytes (from camera capture)
    }
}
