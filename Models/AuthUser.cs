using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class AuthUser
    {
        public string? Id { get; set; }
        public string? FullName { set; get; }
        public string? Password { set; get; }
        public string? Email { set; get; }
        public DateTime RegDate { get; set; } = DateTime.Now;  //Registraton Date ( to change to static )
        public Fridge? Fridge { set; get; }
    }
}
