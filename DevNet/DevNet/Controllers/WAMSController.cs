using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using DevNet.Infrastructure;
using DevNet.Models;
using System.Web.Http.Cors;


namespace DevNet.Controllers
{
    /// <summary>
    /// IMPORTANT: To secure the website, authorization can be implemented to actions in this controller
    /// Since this is a proof-of-concept app, I just didn't bother implementing it
    /// </summary>
    [EnableCors(origins: "https://localhost:44300/", headers: "x-ms-meta-abc, x-ms-meta-data*, x-ms-meta-target*", methods: "Put")]
    public class WAMSController : ApiController
    {
        private WAMSProvider _wamsProvider = null;

        public WAMSController()
        {
            _wamsProvider = new WAMSProvider();
        }

        /// <summary>
        /// Creates a new WAMS asset for the file.
        /// </summary>
        /// <param name="model">CreateAsset model which contains blob file name..</param>
        /// <returns>WAMS asset.</returns>
        [HttpPost]
        public HttpResponseMessage CreateAsset(CreateAssetModel model)
        {
            var asset = new WAMSAssetModel();
            try
            {
                asset = _wamsProvider.CreateWAMSAsset(_wamsProvider.SanitizeFileName(model.FileName));
            }
            catch (Exception e)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new { errorMessage = e.Message });
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, new { asset });
        }

        /// <summary>
        /// Publishes WAMS asset.
        /// </summary>
        /// <param name="model">PublishAsset model which contains id of the asset to be published and blob file name.</param>
        /// <returns>WAMSJobLocators model entity.</returns>
        [HttpPost]
        public HttpResponseMessage PublishAsset(PublishAssetModel model)
        {
            WAMSJobLocatorsModel wamsLocators = null;
            try
            {
                wamsLocators = _wamsProvider.PublishWAMSAsset(model.AssetId, _wamsProvider.SanitizeFileName(model.FileName));
            }
            catch (Exception e)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new { errorMessage = e.Message });
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, new { wamsLocators });
        }
    }
}
