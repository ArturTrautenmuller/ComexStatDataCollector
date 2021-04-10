using System;
using System.Collections.Generic;
using System.Text;

namespace ComexStatCapture
{
    public static class Location
    {
#if DEBUG
        public static string ConfLocation = @"C:\TERZ\Comex Azure\Captura\Config\Conf.json";
#else
         public static string ConfLocation = @"C:\TERZ\Comex Azure\Captura\Config\Conf.json";
#endif
    }
    public class Conf
    {
        public string ApiUrl { get; set; }
        public string ExtractFolder { get; set; }
        public string LogFolder { get; set; }
        public int YearStart { get; set; }
        public int YearEnd { get; set; }
        public int MonthStart { get; set; }
        public int MonthEnd { get; set; }
        public List<Uf> Ufs { get; set; }

    }
}
