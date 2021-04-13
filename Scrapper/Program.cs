using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            GetData();
            Console.ReadLine();
        }

        private static async void GetData()
        {
            var url = "https://findvaccinenow.com/covid19/us/city-Dallas";
            var httpClient = new HttpClient();
            var html =await httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);


            var LocationsHtml = doc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("bs-calltoaction bs-calltoaction-default")).ToList();


            for(int i=0; i < LocationsHtml.Count; i++)
            {
             
                    var locationBox=LocationsHtml[i].Descendants("div")
            .Where(node => node.GetAttributeValue("class", "")
            .Equals("col-md-7 cta-contents")).ToList();

                foreach (var Location in locationBox)
                {
                    Console.WriteLine(
                         Location.Descendants("h1")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("cta-title")).FirstOrDefault().InnerText
                        );
                    Console.WriteLine(
                         Location.Descendants("small")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("time")).FirstOrDefault().InnerText
                        );
                    Console.WriteLine(
                      Location.Descendants("p").FirstOrDefault().InnerText
                     );
                    Console.WriteLine("\n=================================================================================\n");
                }

            }

           

        

            Console.WriteLine();
        }
    }
}
