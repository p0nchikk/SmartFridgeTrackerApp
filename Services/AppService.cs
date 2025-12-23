using Firebase.Auth;
using Firebase.Database;
using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Services
{
    class AppService
    {
        static public List<Product>? products;

        static FirebaseAuthClient? auth;
        static FirebaseClient? client;
        static public AuthCredential? loginAuthUser;

        // SingleTone Pattern
        static private AppService instance;
        static public AppService GetInstance()
        {
            if (instance == null)
            {
                instance = new AppService();
            }
            return instance;
        }
        public AppService()
        {
            // We need a costructor because of :  _instance = new AppService();
        }
    }
}
