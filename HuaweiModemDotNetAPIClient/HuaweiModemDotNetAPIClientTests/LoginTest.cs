using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HuaweiModemDotNetAPIClientTests
{
    [TestClass]
    public class LoginTest
    {

        private huaweisms.data.ApiConfig config;
        private huaweisms.data.ApiCtx ctx;

        [TestInitialize]
        public void init()
        {
            config = new huaweisms.data.ApiConfig();
            config.BaseURL = "http://192.168.8.1";
            ctx = new huaweisms.data.ApiCtx(config);
        }

        [TestMethod]
        public void TestSesTokInfo()
        {
            huaweisms.api.WebServer webServer = new huaweisms.api.WebServer(ctx);
            var res = webServer.SesTokInfo();
            Assert.IsNotNull(res.Response);
            Assert.Equals(res.Type, huaweisms.data.ApiResponse.ApiResponseType.XML);
            Assert.IsTrue(ctx.VerificationTokensCount > 0);
            Assert.IsNotNull(ctx.SessionId);
        }

        [TestMethod]
        public void TestValidLogin()
        {
            string username = "admin";
            string password = "validpass";


            huaweisms.api.User user = new huaweisms.api.User(ctx);

            var res = user.Login(username, password);

            Assert.IsTrue(ctx.LoggedIn);

        }

        [TestMethod]
        public void TestInValidLogin()
        {
            string username = "admin";
            string password = "invalidpass";


            huaweisms.api.User user = new huaweisms.api.User(ctx);

            var res = user.Login(username, password);

            Assert.IsFalse(ctx.LoggedIn);

        }
    }
}
