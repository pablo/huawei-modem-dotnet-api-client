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

            return null;

        }

    }

    public class User
        : API
    {

        public User(ApiCtx ctx)
            : base(ctx)
        {

        }

        public ApiResponse Login(string userName, string password)
        {
            var webServerAPI = new WebServer(Ctx);




            return null;

        }

    }


}