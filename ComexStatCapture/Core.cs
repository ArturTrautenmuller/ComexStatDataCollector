using System;
using System.Collections.Generic;
using System.Text;

namespace ComexStatCapture
{
    public class Core
    {
       public  Dictionary<int,string> Months { get; set; }
       public List<ErrorLog> Errors { get; set; }
       
        public Core()
        {
            this.Errors = new List<ErrorLog>();

            this.Months.Add(1, "Janeiro");
            this.Months.Add(2, "Fevereiro");
            this.Months.Add(3, "Março");
            this.Months.Add(4, "Abril");
            this.Months.Add(5, "Maio");
            this.Months.Add(6, "Junho");
            this.Months.Add(7, "Julho");
            this.Months.Add(8, "Agosto");
            this.Months.Add(9, "Setembro");
            this.Months.Add(10, "Outubro");
            this.Months.Add(11, "Novembro");
            this.Months.Add(12, "Dezembro");
        }

        public void SaveErrorLog(string filePath)
        {
            string header = "Year;Month;UF;Export_Import;ErrorMessage";
            string body = "";
            foreach (ErrorLog error in this.Errors)
            {
                body += $"{error.Year};{error.Month};{error.UF};{error.Export_Import};{error.ErrorMsg}" + Environment.NewLine;
            }

            string content = header + Environment.NewLine + body;
            System.IO.File.WriteAllText(filePath, content);
        }
    }
}
