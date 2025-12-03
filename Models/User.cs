using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class User
    {
        public string? UserName { set; get; }
        public string? Password { set; get; }
        public string? Email { set; get; }
        public DateTime RegDate { get; set; }   //Registraton Date ( to change to static )


    }
}
