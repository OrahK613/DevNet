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

        #region Views

        public ActionResult Cover()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<ApplicationUser> Users = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToListAsync();
           
            string strRecommendedRSSFeedName = "";
            string CallFunction = "";

            RSSFeedModel rssFeed = new RSSFeedModel();

            if (Request.IsAuthenticated)
            {
               
                if (Users != null)
                {

                    foreach (var item in Users)
                    {

                        if (item.UserName == User.Identity.GetUserName())
                        {
                            strRecommendedRSSFeedName = item.RssFeedName;

                            if(strRecommendedRSSFeedName != null)
                            {

                                rssFeed.RssFeed = rssFeed.GetRSSFeed(strRecommendedRSSFeedName);
                                rssFeed.RssFeedName = strRecommendedRSSFeedName;
                        
                                // Call Confirmation Dialog Here
                                rssFeed.CallFunction = "confirmRSS";
                        
                            }
                            break;
                   
                        }

                    }
                }
            }
            else
            {

                rssFeed.RssFeed = null;
                rssFeed.RssFeedName = "";
                rssFeed.CallFunction = "";
             }

            return View(rssFeed);
        }
     

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

        #endregion

        #region Actions


        //
        // POST: /Home/RSSConfirmation
        public async Task<ActionResult> RSSConfirmation()
        {
            RSSFeedModel rssFeed = new RSSFeedModel();

            IEnumerable<ApplicationUser> Users = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToListAsync();

            string strRecommendedRSSFeedName = "";
            string CallFunction = "";

            if (Request.IsAuthenticated)
            {

                if (Users != null)
                {

                    foreach (var item in Users)
                    {

                        if (item.UserName == User.Identity.GetUserName())
                        {
                            strRecommendedRSSFeedName = item.RssFeedName;

                            if (strRecommendedRSSFeedName != null)
                            {

                                rssFeed.RssFeed = rssFeed.GetRSSFeed(strRecommendedRSSFeedName);
                                rssFeed.RssFeedName = strRecommendedRSSFeedName;
                                rssFeed.CallFunction = "confirmRSS";
                                rssFeed.IsConfirmed = "true";

                            }
                            break;

                        }

                    }
                }
            }
            else
            {

                rssFeed.RssFeed = null;
                rssFeed.RssFeedName = "";
                rssFeed.CallFunction = "";
                rssFeed.IsConfirmed = "false";
               
            }

           
            return View("Index", rssFeed);
        }

       

        #endregion

    }
}