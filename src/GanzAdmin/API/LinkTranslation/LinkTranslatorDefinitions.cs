using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

namespace GanzAdmin.API.LinkTranslation
{
    public class LinkTranslatorDefinitions
    {
        public class Result
        {
            public string Url { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string Provider { get; set; }
            public string ProviderIconUrl { get; set; }
        }

        public static Result HandleYoutube(string rawUrl)
        {
            string api = "AIzaSyDVoPY1sWGMt14l8XAmJwOl5C63udeOL8U";
            string vid = "";

            Dictionary<string, string> query = UrlQueries(new Uri(rawUrl));

            if (query.ContainsKey("v"))
            {
                vid = query["v"];

                HttpClient http = new HttpClient();
                Task<string> response = http.GetStringAsync($"https://www.googleapis.com/youtube/v3/videos?key={api}&part=snippet&id={vid}");
                response.Wait();

                string source = response.Result;
                JsonDocument json = JsonDocument.Parse(source, new JsonDocumentOptions { AllowTrailingCommas = true });
                var item = json.RootElement.GetProperty("items")[0];
                string title = item.GetProperty("snippet").GetProperty("title").GetString();
                string description = item.GetProperty("snippet").GetProperty("description").GetString().Replace('\n', ' ');
                return new Result
                {
                    Url = rawUrl,

                    Title = title,
                    Description = description,
                    ImageUrl = $"https://img.youtube.com/vi/{vid}/0.jpg",

                    Provider = "youtube",
                    ProviderIconUrl = "https://s.ytimg.com/yts/img/favicon-vfl8qSV2F.ico",
                };

            }

            throw new Exception();
        }

        public static Result Translate(string url)
        {
            return HandleYoutube(url);
        }

        public static Dictionary<string, string> UrlQueries(Uri url)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if(url.Query.Length > 1)
            {
                string[] parameters = url.Query.Substring(1).Split("&");
                foreach(string parameter in parameters)
                {
                    string[] query = parameter.Split('=');
                    string k = query[0];
                    string v = query.Length > 1 ? query[1] : "";

                    result.Add(k, v);
                }
            }

            return result;
        }
    }
}
