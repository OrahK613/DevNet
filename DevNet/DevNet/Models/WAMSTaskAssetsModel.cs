using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevNet.Models
{
    public class WAMSTaskAssetsModel
    {
        public IAsset EncodedVideoSmall { get; set; }
        public IAsset EncodedVideoMedium { get; set; }
    }
}