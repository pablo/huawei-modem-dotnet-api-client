using huaweisms.data;

namespace huaweisms.api
{

    public abstract class API
    {
        public API(ApiCtx ctx)
        {
            this.Ctx = ctx;
        }

        public ApiCtx Ctx
        {
            get; private set;
        }

    }

    public class SMS
        : API
    {

        public SMS(ApiCtx ctx)
            : base(ctx)
        {

        }

        public void SendSMS()
        {
            if (!Ctx.LoggedIn)
            {
                throw new System.Exception("You need to call User.Login(user,pass) first");
            }

        }

    }

    public class WebServer
        : API
    {

        public WebServer(ApiCtx ctx)
            : base(ctx)
        {

        }

        public ApiResponse SesTokInfo()
        {
            var response = Ctx.Session.HttpGet($"{Ctx.Config.BaseURL}/api/webserver/SesTokInfo");

            if (response.Response.ContainsKey("SesInfo") && response.Response.ContainsKey("TokInfo"))
            {
                // setup context values
                Ctx.TokInfo = response.Response["TokInfo"] as string;
                Ctx.SessionId = (response.Response["SesInfo"] as string).Substring("SessionID=".Length);
                Ctx.Session.CookieContainer.Add(
                    new System.Net.Cookie("SessionID", Ctx.SessionId)
                    {
                        Domain = "192.168.8.1"
                    }
                );
                return response;
            } 
            else
            {
                throw new System.Exception("SesInfo and/or TokInfo missing in response");
            }


        }

    }

    public class User
        : API
    {

        public User(ApiCtx ctx)
            : base(ctx)
        {

        }

        public ApiResponse Login(string username, string password)
        {
            if (Ctx.SessionId == null)
            {
                // first, setup a session
                var webServerAPI = new WebServer(Ctx);
                var innerReponse = webServerAPI.SesTokInfo();
                if (Ctx.SessionId == null)
                {
                    throw new System.Exception("Can't get SessionID. Impossible to continue");
                }
            }

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            var password4 = System.Convert.ToBase64String(plainTextBytes);

            string xml = $@"<?xml version:""1.0"" encoding=""UTF - 8""?>
<request>
<Username>{username}</Username>
<Password>{password4}</Password>
<password_type>4</password_type>
</request>";

            var response = Ctx.Session.HttpPostXML($"{Ctx.Config.BaseURL}/api/user/login", xml);

            return response;

        }

    }


}