﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevNet.Models
{
    public class DevNetHistoricData
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
}