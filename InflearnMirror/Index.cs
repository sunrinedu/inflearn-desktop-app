using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace InflearnMirror
{
    public class API
    {
        public static string SERVER_HOST = "https://3-159laura.pulsedmedia.com/";

        public static async Task<string[]> GetListAsync()
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = SERVER_HOST;
            string data = await wc.DownloadStringTaskAsync("/public-server/list.txt");
            return data.Split(new string[] { "/\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static async Task<JObject> GetInfoAsync(string name)
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = SERVER_HOST;
            string data = await wc.DownloadStringTaskAsync($"/public-server/{name}/info.json");
            return JObject.Parse(data);
        }

        public static async Task<JObject> GetMapAsync(string name)
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = SERVER_HOST;
            string data = await wc.DownloadStringTaskAsync($"/public-server/{name}/map.json");
            return JObject.Parse(data);
        }

        public static string GetVideoLocationAsync(string name, string filename)
        {
            return $"{SERVER_HOST}/public-server/{name}/{filename}.mp4";
        }

        public static async Task<string> GetHtmlAsync(string name, string filename)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.BaseAddress = SERVER_HOST;
                return await wc.DownloadStringTaskAsync($"/public-server/{name}/{filename}.html");
            }
            catch
            {
                return null;
            }
        }
    }
}
