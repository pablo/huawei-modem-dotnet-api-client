using huaweisms.data;

namespace huaweisms.api
{

    public class User
    {

        public ApiCtx QuickLogin(ApiCtx ctx, string userName, string password)
        {



        }

    }

    public class SMS
    {

        public void SendSMS(ApiCtx apiCtx)
        {

        }

    }

    public class WebServer
    {
        public string GetSessionTokenInfo(ApiCtx ctx)
        {

            var apiResponse = ctx.HttpGet($"{ctx.Config.BaseURL}/webserver/SesTokInfo");


            return ";";

        }
    }

}