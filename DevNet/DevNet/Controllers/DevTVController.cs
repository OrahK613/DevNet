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
using System.IO;

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
        public ActionResult FileUpload()
        {
            if (String.IsNullOrEmpty(AppSettings.WamsAccountName) || String.IsNullOrEmpty(AppSettings.WamsAccountKey))
                return RedirectToAction("ConfigError", "DevTV");

            
            return View();
        }

        public ActionResult ConfigError()
        {
            return View();
        }

        // POST: DevTV/FileUpload
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file, string uri)
        {
             if (ModelState.IsValid)
             {
                 if (file == null)
                 {
                     ModelState.AddModelError("File", "Please Upload Your file");
                 }
                else if (file.ContentLength > 0)
                {
                    long MaxContentLength = 1024L * 1024L * 5000L; //5000 MB
                   //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };
                    string[] AllowedFileExtensions = new string[] { ".mp4" };
 
                     if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                     {
                         ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                     }
 
                     else if (file.ContentLength > MaxContentLength)
                     {
                          ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                     }
                      else
                     {
                         //TO:DO
                         var fileName = Path.GetFileName(file.FileName);
                         var path = Path.GetDirectoryName(file.FileName);
                         //var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
                         //file.SaveAs(path);
                         ModelState.Clear();
                         MediaService.InitMediaService(path);
                         ViewBag.Message = "File uploaded successfully";
                     }
                }
             }
             return View();
        }
    }
}



 