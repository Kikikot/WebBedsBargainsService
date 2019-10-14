using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Externals.WebApiSystem
{
    public class WebApi : IWebApi
    {
        public T Get<T>(string url, double? timeOutMilliSeconds = null)
        {
            HttpClient client = new HttpClient();

            if (timeOutMilliSeconds.HasValue && timeOutMilliSeconds.Value > 0)
                client.Timeout = TimeSpan.FromMilliseconds(timeOutMilliSeconds.Value);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response = client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

            else
                throw new Exception(String.Format("[Http Error --> StatusCode: {0} - {1} | Url: {2}]", (int)response.StatusCode, response.StatusCode, url));
        }
    }
}
