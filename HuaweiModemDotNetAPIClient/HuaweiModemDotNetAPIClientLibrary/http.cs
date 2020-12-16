

using huaweisms.data;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace huaweisms.http
{

    public class HttpSession
    {

        public HttpSession(ApiConfig apiConfig)
        {
            this.ApiConfig = apiConfig;


            // setup http client
            CookieContainer = new CookieContainer();
            HttpClientHandler = new HttpClientHandler();
            HttpClientHandler.CookieContainer = CookieContainer;
            HttpClient = new HttpClient(HttpClientHandler);
            HttpClientHandler.UseCookies = true;
            CookieContainer.Add(new Cookie("testc", "ookie")
            {
                Domain = "192.168.8.1"
            });
            HttpClient.Timeout = TimeSpan.FromMilliseconds(apiConfig.HttpTimeout);
        }

        public ApiConfig ApiConfig
        {
            get; private set;
        }

        public HttpClient HttpClient
        {
            get; private set;
        }

        public CookieContainer CookieContainer
        {
            get; private set;
        }

        public HttpClientHandler HttpClientHandler
        {
            get; private set;
        }


        public ApiResponse HttpGet(String url)
        {
            var response = HttpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return ApiResponse.ReadAndCreateApiResponse(response);
            }
            throw new Exception("HTTP request failed: " + response.ReasonPhrase);
        }

        internal ApiResponse HttpPostXML(string url, string xml)
        {
            HttpContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = HttpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return ApiResponse.ReadAndCreateApiResponse(response);
            }
            throw new Exception("HTTP request failed: " + response.ReasonPhrase);
        }
    }

}
