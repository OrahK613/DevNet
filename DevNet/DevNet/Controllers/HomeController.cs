using DevNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DevNet.Controllers
{
  
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            InvokeRequestResponseService().Wait();
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, AutoPriceWebService>() { 
                        { 
                            "input1", 
                            new AutoPriceWebService() 
                            {
                                ColumnNames = new string[] {"make", "body-style", "wheel-base", "engine-size", "horsepower", "peak-rpm", "highway-mpg", "price"},
                                Values = new string[,] {  { "value", "value", "0", "0", "0", "0", "0", "0" },  { "value", "value", "0", "0", "0", "0", "0", "0" },  }
                            }
                        },
                                        },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "NJYRbTJFaeEoVA4JvkmuKIRepVdvJ6UFMaYYTjcYizgdqV1pd9TMEL77Shu+zKvOAcYA/dZuQeeUgY2DUp7Cnw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e9f90a212fdc446f99acab120ed88a0a/services/7878c88be93640cab6b38c92d76bff8c/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine("Failed with status code: {0}", response.StatusCode);
                }
            }
        }
    }
}