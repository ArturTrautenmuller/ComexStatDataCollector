using System;
using System.Collections.Generic;
using System.Text;

namespace ComexStatCapture
{
    public class UrlParameters
    {
        public string ImportExport { get; set; }  // 1 - export, 2 - import
        public string YearStart { get; set; }
        public string YearEnd { get; set; }
        public string MonthStart { get; set; }
        public string MonthEnd { get; set; }
        public string MonthStartName { get; set; }
        public string MonthEndName { get; set; }
        public string UfId { get; set; } 

    }
}
