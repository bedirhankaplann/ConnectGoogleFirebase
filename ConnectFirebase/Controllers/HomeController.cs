using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using ConnectFirebase.Models;
using System.Security.Cryptography;
using System.Text;

namespace ConnectFirebase.Controllers
{
    
     public class HomeController : Controller
     {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "Database KEY",
            BasePath = "Database URL"
        };

        IFirebaseClient client;
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String username, string email, string password)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                List<Index> person = new List<Index>();

                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    password = builder.ToString();
                }
                    
                    DatabaseModel personRegister = new DatabaseModel
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                var set = client.Set(@"Register Information/", personRegister);

            }catch(Exception e)
            {
                _ = e.InnerException;
            }
            return View();
        }
    }
}
