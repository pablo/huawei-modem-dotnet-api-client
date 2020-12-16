

using huaweisms.data;
using System;
using System.Net.Http;

namespace huaweisms.http
{

    public class HttpSession
    {

        public HttpSession(ApiConfig apiConfig)
        {
            this.ApiConfig = apiConfig;


            // setup http client
            HttpClient = new HttpClient();
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

        public ApiResponse HttpGet(String url)
        {
            var response = HttpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return ApiResponse.ReadAndCreateApiResponse(response);
            }
            throw new Exception("HTTP request failed: " + response.ReasonPhrase);
        }

        public void QuickLogin(string user, string password)
        {

        }

        public string GetSessionTokenInfo(ApiCtx ctx)
        {

            var apiResponse = HttpClient.GetAsync($"{ctx.Config.BaseURL}/webserver/SesTokInfo").Result;

            if (apiResponse.IsSuccessStatusCode)
            {
                string content = apiResponse.Content.ReadAsStringAsync().Result;


            }




            return ";";

        }



    }

}
