

namespace huaweisms.http
{

    public class HttpSession
    {

        public HttpSession(ApiConfig apiConfig)
        {
            this.ApiConfig = apiConfig;
            this.LoggedIn = false;


            // setup http client
            HttpClient = new HttpClient();
            HttpClient.Timeout = TimeSpan.FromMilliseconds(config.HttpTimeout);

        }

        public ApiConfig ApiConfig
        {
            get; private set;
        }

        public HttpClient HttpClient
        {
            get; private set;
        }

        public string SessionId
        {
            get; set;
        }

        public string LoginToken
        {
            get; set;
        }

        public bool LoggedIn
        {
            get; set;
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

            var apiResponse = ctx.HttpGet($"{ctx.Config.BaseURL}/webserver/SesTokInfo");




            return ";";

        }



    }

}
