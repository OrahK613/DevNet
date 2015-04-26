using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.WebHost;
using DevNet.Infrastructure;
using DevNet.Models;

namespace DevNet.Controllers
{
    public class DevTVController : Controller
    {
        
        // GET: DevTV
        public ActionResult DevTV()
        {
            return View();
        }

        // GET: Upload
        public ActionResult Upload()
        {
            if (String.IsNullOrEmpty(AppSettings.WamsAccountName) || String.IsNullOrEmpty(AppSettings.WamsAccountKey))
                return RedirectToAction("ConfigError", "DevTV");

            
            return View();
        }

        public ActionResult ConfigError()
        {
            return View();
        }
    }
}