using System;
using System.Collections.Generic;
using System.Text;

namespace ComexStatCapture
{
    public class List
    {
        public string coAno { get; set; }
        public string noPaispt { get; set; }
        public string noUf { get; set; }
        public string coSh2 { get; set; }
        public string noSh2pt { get; set; }
        public string coNcm { get; set; }
        public string noNcmpt { get; set; }
        public string coSh4 { get; set; }
        public string noSh4pt { get; set; }
        public string coSh6 { get; set; }
        public string noSh6pt { get; set; }
        public string noBlocopt { get; set; }
        public string noUrf { get; set; }
        public string vlFob { get; set; }
    }

    public class Data
    {
        public List<List> list { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
        public bool success { get; set; }
        public object message { get; set; }
        public object processo_info { get; set; }
    }
}
