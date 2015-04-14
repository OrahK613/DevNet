using DevNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Data.Entity;


namespace DevNet.Controllers
{
    
    public class HomeController : Controller
    {
        // public RSSFeedModel rssFeed = new RSSFeedModel();

        public ActionResult Cover()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            return View(await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToListAsync());
        }
     
       // [AllowAnonymous]
        //public ActionResult Index()
        //{
        //    //InvokeRequestResponseService().Wait();

        //    //Newtonsoft.Json.Linq.JObject RecommendedRSSFeed = new Newtonsoft.Json.Linq.JObject();

        //    //RecommendedRSSFeed = DevNetAnalyticsViewModel.RSSFeed;

        //    //string strRecommendedRSSFeed = RecommendedRSSFeed["Results"]["Recommended RSS Feed"]["value"]["Values"][0][12].ToString();

        //    // Just Added RSS Feed Stuff
        //    // http://www.docstorus.com/viewer.aspx?code=6db72a9d-b825-4d49-8bc1-267891dc1d14

        //    //ViewData["rssFeed"] = rssFeed.GetRSSFeed(strRecommendedRSSFeed);

        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "CPDM 290 - Capstone Project";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Manage()
        {
            ViewBag.Message = "Manage site here.";

            return View();
        }

        //static async Task InvokeRequestResponseService()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var scoreRequest = new
        //        {

        //            Inputs = new Dictionary<string, DevNetAnalytics>() { 
        //                { 
        //                    "Dev Profile Parameters", 
        //                    new DevNetAnalytics() 
        //                    {
        //                        ColumnNames = new string[] {"date Of Birth", "Favorite IDE", "Software Specialty", "Programming Language"},
        //                        Values = new string[,] {  { "01/28/1979", "FP Haskell Center", "Parallel Programming", "Haskell" }  }
                     
        //                    }
        //                },
        //                                },
        //            GlobalParameters = new Dictionary<string, string>()
        //            {
        //            }
        //        };
        //        const string apiKey = "71p+44dA6qfaXtMDPOqOzfJFO4T2H3grzjPLT3+0VxMPTmYxVrQUL3XC0Hl/h73eKABsbWO4ITH+juwA1oNDzQ=="; // Replace this with the API key for the web service
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        //        client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e9f90a212fdc446f99acab120ed88a0a/services/4e67ba199fa946f8a9890785707cd163/execute?api-version=2.0&details=true");
                
        //        // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
        //        // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
        //        // For instance, replace code such as:
        //        //      result = await DoSomeTask()
        //        // with the following:
        //        //      result = await DoSomeTask().ConfigureAwait(false)


        //        HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

               

        //        if (response.IsSuccessStatusCode)
        //        {
        //            //string result = await response.Content.ReadAsStringAsync();
        //            //Console.WriteLine("Result: {0}", result);

        //            var lstRSSFeeds = await response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
        //            {

        //                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(readTask.Result);

        //                return jObject;

        //            });

        //            DevNetAnalyticsViewModel.RSSFeed = lstRSSFeeds;
        //        }
        //        else
        //        {
        //            Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

        //            // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
        //            Console.WriteLine(response.Headers.ToString());

        //            string responseContent = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(responseContent); 
        //        }
        //    }
        //}
    }
}