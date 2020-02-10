using Services.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class HttpClientService : HttpClient, IHttpClientService
    {
        public HttpClientService() : base()
        {
            Timeout = TimeSpan.FromSeconds(180);
            MaxResponseContentBufferSize = 50000000; // 50MB
        }
        
        public async Task<Stream> DownloadFile(string url)
        {
            var uri = new Uri(url);
            var response = await GetAsync(uri);
            if (response.IsSuccessStatusCode)
               return await response.Content.ReadAsStreamAsync();

            return null;
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
