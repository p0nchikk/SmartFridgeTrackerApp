using Firebase.Auth;
using Firebase.Database;
using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFridgeTracker.Models;
using Firebase.Auth.Providers;
using Firebase.Database.Query;
using Microsoft.Maui.ApplicationModel.Communication;

namespace SmartFridgeTracker.Services
{
    class AppService
    {
        public List<Product>? products;

        //Firebase
        FirebaseAuthClient? auth;
        FirebaseClient? client;
        public AuthCredential? loginAuthUser;
        public AuthUser fullDetailsLoggedInUser; //ask if it's ok to name user model as "User" or to change to "AuthUser"

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
       
        public void Init()
        {
            var config = new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyBgNrU9XdtYiZpruPDGWZ9gtjzhOo9IG9Q", //Unique API Key
                AuthDomain = "smartfridgetracker-555df.firebaseapp.com", //Loggin address 
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[] //List of authentication variations
                {
                    new EmailProvider() //Authentication via email
                }
            };
            auth = new FirebaseAuthClient(config);

            client =
                new FirebaseClient(@"https://smartfridgetracker-555df-default-rtdb.europe-west1.firebasedatabase.app",
                new FirebaseOptions
                { 
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.User.Credential.IdToken) //indicate aurhentication with cloud
                });
        }

        //Registration
        public async Task<bool> TryRegister(string email, string password, string fullName)
        {
            try
            {
                var respond = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
                //User is signed up and logged in
                Fridge userFridge = new Fridge();
                fullDetailsLoggedInUser = new AuthUser()
                {
                    Email = respond.User.Info.Email,
                    Id = respond.User.Uid,
                    FullName = fullName,
                    Fridge = userFridge, 
                    RegDate = DateTime.Now
                };
                DateTime regDate = fullDetailsLoggedInUser.RegDate;

                await client //await client
                   .Child("users")
                   .Child(fullDetailsLoggedInUser.Id)
                   .PutAsync(new 
                    {
                        email = email,
                        fullName = fullName, 
                        fridge = userFridge,
                        regDate = regDate.ToString("o") //ISO 8601 format
                   });
                

                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     ex.Message,
                     "OK"
                     );
                return false;
            }
        }

        //Login
        public async Task<bool> TryLogin(string username, string password)
        {
            if( username == null || password == null )
            {
                return false;
            }
            try
            {
                var authUser = await auth.SignInWithEmailAndPasswordAsync(username, password); //try logging in
                loginAuthUser = authUser.AuthCredential; //verification access to system

                //Load additional user data from database
                string uid = auth.User.Uid;
                string fullName = await client
                   .Child("users")
                   .Child(uid)
                   .Child("fullName")
                   .OnceSingleAsync<string>();

                //Load registration date
                string? regDateString = await client
                   .Child("users")
                   .Child(uid)
                   .Child("regDate")
                   .OnceSingleAsync<string>();

                //Retreive products from fridge
                var productsSnapshot = await client
                .Child("users")
                .Child(uid)
                .Child("fridge")
                .Child("products")
                .OnceAsync<Product>();

                Fridge userFridge = new Fridge();

                foreach (var item in productsSnapshot)
                {
                    userFridge.ProductsList.Add(item.Object);
                }

                //Sync data to user model
                fullDetailsLoggedInUser = new AuthUser() //sync user to user model
                {
                    Email = auth.User.Info.Email,
                    Id = auth.User.Uid,
                    FullName = fullName,
                    Fridge = userFridge,
                    RegDate = DateTime.Parse(regDateString)
                };
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Logout()
        {
            try
            {
                auth.SignOut(); //sign out
                loginAuthUser = null; //deactivate user
                fullDetailsLoggedInUser = null; //deactivate user model
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Fridge

        public async Task<bool> LoadProduct(Product product)
        {
            if (client == null || product == null || fullDetailsLoggedInUser == null)
                return false;

            string uid = fullDetailsLoggedInUser.Id;

            // Generate unique key
            string productId = Guid.NewGuid().ToString();

            await client
                .Child("users")
                .Child(uid)
                .Child("fridge")
                .Child("products")
                .Child(productId)
                .PutAsync(product);

            // Update local memory
            fullDetailsLoggedInUser.Fridge.ProductsList.Add(product);

            return true;
        }

    }
}
