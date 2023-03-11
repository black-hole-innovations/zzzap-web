using Kaje.ServiceModel;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaje.ServiceInterface
{
    public class KajeClient
    {
        private string baseUrl = "https://zzzap.io";
        public KajeClient() { }

        public async Task<KajeResponse<string>> AlphabetizeList(List<string> items)
        {
            var url = baseUrl.AppendUrlPaths("Utilities", "sorting", "alphabetize");
            url = url.AddQueryParam("list", string.Join(',', items));
            var json = await url.GetJsonFromUrlAsync();
            return json.FromJson<KajeResponse<string>>();
        }

        public async Task<KajeResponse<List<T>>> DataFind<T>(string query)
        {
            var url = baseUrl.AppendUrlPaths("Collections", "dataFind");
            url = url.AddQueryParam("search", query);
            var json = url.GetJsonFromUrl();

            return json.FromJson<KajeResponse<List<T>>>();
        }

        public async Task<KajeResponse<List<T>>> DataPush<T>(string name, string group, T item) //where T: KajeBaseItem<T>
        {
            var url = baseUrl.AppendUrlPaths("Collections", "dataPush");
            url = url.AddQueryParam("name", name).AddQueryParam("group", group);
            var reqJson = string.Empty;
            
            using (JsConfig.With(new Config { EmitCamelCaseNames=true }))
            {
                reqJson = item.ToJson();
            }
            url = url.AddQueryParam("string", reqJson);

            var json = url.GetJsonFromUrl();
            //var req = new
            //{
            //    name = name,
            //    group = group,
            //    json = item
            //};
            //var reqJson = string.Empty;
            //using (JsConfig.With(new Config { EmitCamelCaseNames=true }))
            //{
            //    reqJson = req.ToJson();
            //}
            //var json = await url.PostJsonToUrlAsync(reqJson);

            return json.FromJson<KajeResponse<List<T>>>();

             
        }

    }



    public class KajeServices : Service
    {
        public async Task<List<string>> Any(KajeSort req)
        {
            var kj = new KajeClient();
            var response = await kj.AlphabetizeList(req.Unsorted);
            return response.response.FromJson<List<string>>();

        }

        public async Task<KajeItem> Any(KajeAddItem req)
        {
            KajeClient kj = new KajeClient();
            var push = new KajeItem() { Data = req };
            var response = await kj.DataPush<KajeItem>("@sbworld", "address", push);
            return response.response.First();
        }

        public async Task<object> Any(KajeQueryItems req)
        {
            KajeClient kj = new KajeClient();
            var r = await kj.DataFind<KajeItem>("@sbworld");
            return r.response;
        }

    }
}
