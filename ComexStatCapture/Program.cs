using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Net;



namespace ComexStatCapture
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            Core core = new Core();

            DateTime Date = new DateTime(conf.YearStart,conf.MonthStart,1);
            DateTime DateEnd = new DateTime(conf.YearEnd,conf.MonthEnd,1);

            while(DateTime.Compare(Date,DateEnd) <= 0)
            {

                foreach (Uf uf in conf.Ufs)
                {
                    // Exports

                    Console.WriteLine($"Extracting Year:{Date.Year.ToString()} Month:{Date.Month.ToString("00")} UF:{uf.Name}");

                    UrlParameters exportParameters = new UrlParameters();
                    exportParameters.ImportExport = "1";
                    exportParameters.MonthStart = Date.Month.ToString("00");
                    exportParameters.MonthEnd = Date.Month.ToString("00");
                    exportParameters.YearEnd = Date.Year.ToString();
                    exportParameters.YearStart = Date.Year.ToString();
                    exportParameters.UfId = uf.Id;
                    exportParameters.MonthStartName = core.Months[Date.Month];
                    exportParameters.MonthEndName = core.Months[Date.Month];

                    string ExportUrl = conf.ApiUrl + ComexClient.BuildUrlParameters(exportParameters);
                  
                    try
                    {
                       Root ExportData = ComexClient.getData(ExportUrl);
                       string ExportFile = System.IO.Path.Combine(conf.ExtractFolder, "Exports", Date.Year.ToString(), Date.Month.ToString("00"), $"{uf.Name}_{Date.Year.ToString()}_{Date.Month.ToString("00")}");
                       ComexClient.SaveFile(ExportData, ExportFile);
                    }
                    catch(Exception e)
                    {
                        core.Errors.Add(new ErrorLog() {
                            Year = Date.Year.ToString(),
                            Month = Date.Month.ToString("00"),
                            UF = uf.Name,
                            Export_Import = "Exports",
                            ErrorMsg = e.Message
                        });
                    }
                    
                   
                    // Imports

                    UrlParameters importParameters = new UrlParameters();
                    importParameters.ImportExport = "2";
                    importParameters.MonthStart = Date.Month.ToString("00");
                    importParameters.MonthEnd = Date.Month.ToString("00");
                    importParameters.YearEnd = Date.Year.ToString();
                    importParameters.YearStart = Date.Year.ToString();
                    importParameters.UfId = uf.Id;
                    importParameters.MonthStartName = core.Months[Date.Month];
                    importParameters.MonthEndName = core.Months[Date.Month];

                    string ImportUrl = conf.ApiUrl + ComexClient.BuildUrlParameters(importParameters);

                    try
                    {
                        Root ImportData = ComexClient.getData(ImportUrl);
                        string ImportFile = System.IO.Path.Combine(conf.ExtractFolder, "Imports", Date.Year.ToString(), Date.Month.ToString("00"), $"{uf.Name}_{Date.Year.ToString()}_{Date.Month.ToString("00")}");
                        ComexClient.SaveFile(ImportData, ImportFile);
                    }

                    catch (Exception e)
                    {
                        core.Errors.Add(new ErrorLog()
                        {
                            Year = Date.Year.ToString(),
                            Month = Date.Month.ToString("00"),
                            UF = uf.Name,
                            Export_Import = "Imports",
                            ErrorMsg = e.Message
                        });
                    }


                }

                Date.AddMonths(1);
            }


            core.SaveErrorLog(System.IO.Path.Combine(conf.LogFolder,"Errors.txt"));

            Console.WriteLine("Finished");
        }
    }
}
