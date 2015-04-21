using DevNet.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevNet.Controllers
{
    public class DashboardController : Controller
    {
        #region Views

        // GET: Dashboard
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> Dashboard()
      
        public ActionResult Dashboard()
        {
            DashboardViewModel model = new DashboardViewModel();
            
            // Call Web Servic to get recommended RSS Feed Info
            InvokeRequestResponseService().Wait();

            // Get the JObject 
            Newtonsoft.Json.Linq.JObject joHistoricData = new Newtonsoft.Json.Linq.JObject();

            // Get the actual historic records
            joHistoricData = DevNetHistoricDataViewModel.HistoricData;
            JArray jaDateOfBirth = (JArray)joHistoricData["Results"]["Dev Net Historic Data"]["value"]["Values"][0];
            IList<string> lstValues = jaDateOfBirth.Select(v => (string)v).ToList();

            // Add to our Dashboard view model
            model.HistoricData = joHistoricData;

            return View(model);
        }

        #endregion

        #region Actions

       // [AllowAnonymous]
        static async Task InvokeRequestResponseService()
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                using (var client = new HttpClient())
                {

                    var scoreRequest = new
                    {
                        GlobalParameters = new Dictionary<string, string>()
                        {
                        }
                    };

                    const string apiKey = "0AVIAEPQB8NPC4UhY86NrD9hMlV2cTaCn/wx+6+fpXKr7Hvk6YclAu/MDSv0rd0YEgJgKo/5vaFQC0oY+e+KiQ=="; // Replace this with the API key for the web service
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e9f90a212fdc446f99acab120ed88a0a/services/18b8160846674fd7a3c30f0ee4071030/execute?api-version=2.0&details=true");

                    // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                    // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                    // For instance, replace code such as:
                    //      result = await DoSomeTask()
                    // with the following:
                    //      result = await DoSomeTask().ConfigureAwait(false)


                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);



                    if (response.IsSuccessStatusCode)
                    {
                        //string result = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Result: {0}", result);

                        var lstDevNetHistoricData = await response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
                        {

                            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(readTask.Result);

                            return jObject;

                        });

                        DevNetHistoricDataViewModel.HistoricData = lstDevNetHistoricData;
                    }
                    else
                    {
                        Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                        // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                        Console.WriteLine(response.Headers.ToString());

                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                }
            }
        }

        #endregion

    }
}