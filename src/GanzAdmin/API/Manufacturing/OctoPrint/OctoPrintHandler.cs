using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GanzAdmin.API.Manufacturing.OctoPrint
{
    public class OctoPrintHandler
    {
        public string ApiKey { get; set; }
        public string Address { get; set; }

        public OctoPrintHandler(string address, string key)
        {
            this.Address = address;
            this.ApiKey = key;
        }

        public void Test()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(this.Address)
            };

            Task<string> json = client.GetStringAsync("/api/job");
            json.Wait();

            dynamic dynJson = JsonConvert.DeserializeObject(json.Result);
            foreach (var item in dynJson)
            {
                Console.WriteLine("{0} {1} {2} {3}\n", item.id, item.displayName,
                    item.slug, item.imageUrl);
            }
        }
    }
}
