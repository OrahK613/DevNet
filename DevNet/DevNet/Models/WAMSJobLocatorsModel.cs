using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevNet.Models
{
    public class WAMSJobLocatorsModel
    {
        public WAMSLocatorModel OriginalVideo { get; set; }
        public WAMSLocatorModel EncodedVideoSmall { get; set; }
        public WAMSLocatorModel EncodedVideoMedium { get; set; }
    }
}