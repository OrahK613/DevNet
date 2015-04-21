using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace DevNet.Models
{
    public class DashboardViewModel
    {
       // public Newtonsoft.Json.Linq.JObject HistoricData { get; set; }
       // public List<IList<string>> HistoricData { get; set; }
       // public List<Dashboard> HistoricData { get; set; }
        public PagedList<Dashboard> HistoricData { get; set; }
    }
}