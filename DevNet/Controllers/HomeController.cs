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
using Newtonsoft.Json;

namespace DevNet.Controllers
{

    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {

            // List<Automobile> lstAutomobiles = null;

            InvokeRequestResponseService().Wait();

            //ViewData["lstAutomobiles"] = lstAutomobiles;

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
                                Values = new string[,] {  { "audi", "sedan", "99.8", "109", "102", "5500", "30", "13495" },  { "audi", "sedan", "97.5", "112", "130", "4700", "35", "12700" },  }

                     
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


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    // string result = await response.Content.ReadAsStringAsync();



                    var lstAutomobiles = await response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
                    {

                        var result = JsonConvert.DeserializeObject(readTask.Result);


                        return result;

                        // lstAutomobiles = JsonConvert.DeserializeObject<List<Automobile>>(readTask.Result, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

                        //return lstAutomobiles;
                    });

                    AutoViewModel.Automobiles = lstAutomobiles;

                    //ViewBag["lstAutomobiles"] = lstAutomobiles;
                    // Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine("Failed with status code: {0}", response.StatusCode);
                }
            }
        }
    }
}