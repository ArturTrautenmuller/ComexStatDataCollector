using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using RestSharp;
using Newtonsoft.Json;

namespace ComexStatCapture
{
    public static class ComexClient
    {
        public static Root getData(string url)
        {
            //  var client = new RestClient("http://api.comexstat.mdic.gov.br/print/export?filter=%7B%22yearStart%22:%222020%22,%22yearEnd%22:%222020%22,%22typeForm%22:1,%22typeOrder%22:2,%22filterList%22:%5B%5D,%22filterArray%22:%5B%5D,%22rangeFilter%22:%5B%5D,%22detailDatabase%22:%5B%5D,%22monthDetail%22:true,%22metricFOB%22:true,%22metricKG%22:false,%22metricStatistic%22:false,%22monthStart%22:%2201%22,%22monthEnd%22:%2212%22,%22formQueue%22:%22general%22,%22langDefault%22:%22pt%22,%22monthStartName%22:%22Janeiro%22,%22monthEndName%22:%22Dezembro%22,%22columnsTable%22:%5B%7B%22text%22:%22Ano%22,%22id%22:%22coAno%22%7D,%7B%22text%22:%22M%C3%AAs%22,%22id%22:%22coMes%22%7D,%7B%22text%22:%222020%20-%20Valor%20FOB%20(US$)%22,%22id%22:%22vlFob%22%7D%5D,%22exportType%22:2,%22typeTable%22:1,%22fixedIndex%22:%5B%22coMes%22%5D,%22fixedContent%22:%5B%22coMes%22%5D,%22returnExcel%22:true%7D");
            var client = new RestClient(url);

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return JsonConvert.DeserializeObject<Root>(response.Content);
        }

        public static void SaveFile(Root root, string PathFile)
        {
            string header = "Pais;Uf;SH2;SH4;SH6;NCM;Bloco;URF;Valor";
            string body = "";
            foreach(List list in root.data.list)
            {
                body += $"{list.noPaispt};{list.noUf};{list.noSh2pt};{list.noSh4pt};{list.noSh6pt},{list.noBlocopt},{list.noUrf},{list.vlFob}" + Environment.NewLine;
            }

            string content = header + Environment.NewLine + body;
            System.IO.File.WriteAllText(PathFile, content);
        }

        public static string BuildUrlParameters(UrlParameters urlParameters)
        {
            return $"?filter=%7B%22yearStart%22:%22{urlParameters.YearStart}%22,%22yearEnd%22:%22{urlParameters.YearEnd}%22,%22typeForm%22:{urlParameters.ImportExport},%22typeOrder%22:1,%22filterList%22:%5B%7B%22id%22:%22noUf%22,%22text%22:%22UF%20do%20Produto%22,%22route%22:%22/pt/location/states%22,%22type%22:%221%22,%22group%22:%22gerais%22,%22groupText%22:%22Gerais%22,%22hint%22:%22fieldsForm.general.noUf.description%22,%22placeholder%22:%22UFs%20do%20Produto%22%7D%5D,%22filterArray%22:%5B%7B%22item%22:%5B%22{urlParameters.UfId}%22%5D,%22idInput%22:%22noUf%22%7D%5D,%22rangeFilter%22:%5B%5D,%22detailDatabase%22:%5B%7B%22id%22:%22noPaispt%22,%22text%22:%22Pa%C3%ADs%22%7D,%7B%22id%22:%22noUf%22,%22text%22:%22UF%20do%20Produto%22%7D,%7B%22id%22:%22noSh2pt%22,%22text%22:%22Cap%C3%ADtulo%20(SH2)%22,%22parentId%22:%22coSh2%22,%22parent%22:%22Codigo%20SH2%22%7D,%7B%22id%22:%22noNcmpt%22,%22text%22:%22NCM%20-%20Nomenclatura%20Comum%20do%20Mercosul%22,%22parentId%22:%22coNcm%22,%22parent%22:%22C%C3%B3digo%20NCM%22%7D,%7B%22id%22:%22noSh4pt%22,%22text%22:%22Posi%C3%A7%C3%A3o%20(SH4)%22,%22parentId%22:%22coSh4%22,%22parent%22:%22Codigo%20SH4%22%7D,%7B%22id%22:%22noSh6pt%22,%22text%22:%22Subposi%C3%A7%C3%A3o%20(SH6)%22,%22parentId%22:%22coSh6%22,%22parent%22:%22Codigo%20SH6%22%7D,%7B%22id%22:%22noBlocopt%22,%22text%22:%22Bloco%20Econ%C3%B4mico%22%7D,%7B%22id%22:%22noUrf%22,%22text%22:%22URF%22%7D%5D,%22monthDetail%22:false,%22metricFOB%22:true,%22metricKG%22:false,%22metricStatistic%22:false,%22monthStart%22:%22{urlParameters.MonthStart}%22,%22monthEnd%22:%22{urlParameters.MonthEnd}%22,%22formQueue%22:%22general%22,%22langDefault%22:%22pt%22,%22monthStartName%22:%22{urlParameters.MonthStartName}%22,%22monthEndName%22:%22{urlParameters.MonthEndName}%22%7D" ;
        }

        
    }
}
