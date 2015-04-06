using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages.Html;

namespace DevNet.Models
{
    public class State
    {
        
        public Int32 StateID { get; set; }
        public String StateName { get; set; }
        public String StateAbbreviation { get; set; }

    }
}