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
using PagedList;

namespace DevNet.Controllers
{
    public class DashboardController : Controller
    {
        #region Views

        // GET: Dashboard
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> Dashboard()
      
        public ActionResult Dashboard(string sortOrder, int? page)
        {
            DashboardViewModel model = new DashboardViewModel();
            
            // sorting
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.IDESortParam = String.IsNullOrEmpty(sortOrder) ? "ide_desc" : "";
            ViewBag.SoftwareSortParam = String.IsNullOrEmpty(sortOrder) ? "software_desc" : "";
            ViewBag.ProgrammingSortParam = String.IsNullOrEmpty(sortOrder) ? "prog_desc" : "";
            ViewBag.RssSortParam = String.IsNullOrEmpty(sortOrder) ? "rss_desc" : "";

            // paging
            ViewBag.CurrentSort = sortOrder; // This is to maintain sorting from page to page


              
            // Call Web Servic to get recommended RSS Feed Info
            InvokeRequestResponseService().Wait();

            // Get the JObject 
            Newtonsoft.Json.Linq.JObject joHistoricData = new Newtonsoft.Json.Linq.JObject();

            // Get the actual historic records
            joHistoricData = DevNetHistoricDataViewModel.HistoricData;
           
            List<IList<string>> lstData = new List<IList<string>>();

            // Convert JObject into a list of list objects
            foreach (JArray record in joHistoricData["Results"]["Dev Net Historic Data"]["value"]["Values"])
            {
                IList<string> lstRecord = record.Select(v => (string)v).ToList();
                lstData.Add(lstRecord);
       
            }

            // Convert the list objects in instances of ApplicationUser 
            List<Dashboard> lstDashboard = new List<Dashboard>();
            
            foreach(IList<string> list in lstData)
            {
                Dashboard item = new Dashboard();
                item.DateOfBirth = Convert.ToDateTime(list[0]);
                item.FavoriteIDEName = list[1];
                item.SoftwareSpecialtyName = list[2];
                item.ProgrammingLanguageName = list[3];
                item.PreferredRSSFeed = list[4];

                lstDashboard.Add(item);
            }

            var dashboardRecords = from d in lstDashboard select d;

            switch(sortOrder)
            {
                case "Date":
                    dashboardRecords = dashboardRecords.OrderBy(d => d.DateOfBirth);
                    break;
                case "ide_desc":
                    dashboardRecords = dashboardRecords.OrderByDescending(d => d.FavoriteIDEName);
                    break;
                case "software_desc":
                    dashboardRecords = dashboardRecords.OrderBy(d => d.SoftwareSpecialtyName);
                    break;
                case "prog_desc":
                    dashboardRecords = dashboardRecords.OrderBy(d => d.ProgrammingLanguageName);
                    break;
                case "rss_desc":
                    dashboardRecords = dashboardRecords.OrderBy(d => d.PreferredRSSFeed);
                    break;
                default:
                    dashboardRecords = dashboardRecords.OrderBy(d => d.SoftwareSpecialtyName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1); // null-coalescing operator defines a default value for a nullable type


            // Add to our Dashboard view model
           // model.HistoricData = dashboardRecords.ToPagedList(pageNumber, pageSize);

            return View(dashboardRecords.ToPagedList(pageNumber, pageSize));
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