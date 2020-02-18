using Services.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Services
{
    public class HttpClientService : HttpClient, IHttpClientService
    {
        public HttpClientService() : base()
        {
            ServicePointManager.DefaultConnectionLimit = 20;    // 20 requests
            Timeout = TimeSpan.FromSeconds(180);                // 3 min
            MaxResponseContentBufferSize = 100000000;           // 100MB
        }
        
        public async Task<Stream> DownloadFile(string url)
        {
            var uri = new Uri(url);
            var response = await GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
               return await response.Content.ReadAsStreamAsync();

            return null;
        }

        public async Task<string> Get(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pt"));
                client.DefaultRequestHeaders.Host = "api.bcb.gov.br";
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.106 Safari/537.36");
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                return await client.GetStringAsync(url);
            }
        }

        //public async Task<List<T>> GetListItems<T>(string url)
        //{
        //    var uri = new Uri(url);
        //    var response = await GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var items = JsonConvert.DeserializeObject<List<T>>(content);
        //        return items;
        //    }
        //    throw new Exception(response.ReasonPhrase);
        //}

        //public async Task<T> GetItem<T>(string url)
        //{
        //    var uri = new Uri(url);
        //    var response = await GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        //var item = JsonConvert.DeserializeObject<T>(content);
        //        //return item;
        //        return T;
        //    }
        //    throw new Exception(response.ReasonPhrase);
        //}

        //public async Task PostItem<T>(T item, string url)
        //{
        //    var uri = new Uri(url);
        //    //var json = JsonConvert.SerializeObject(item);
        //    var json = "";
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = null;
        //    response = await PostAsync(uri, content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return;
        //    }
        //    throw new Exception(response.ReasonPhrase);
        //}
    }
}
