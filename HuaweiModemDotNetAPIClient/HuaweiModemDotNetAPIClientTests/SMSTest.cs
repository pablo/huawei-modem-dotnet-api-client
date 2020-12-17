using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HuaweiModemDotNetAPIClientTests
{
    [TestClass]
    public class SMSTest
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
        public void TestSend2SMS()
        {
            string username = "admin";
            string password = "validpass";

            huaweisms.api.User user = new huaweisms.api.User(ctx);
            huaweisms.api.SMS sms = new huaweisms.api.SMS(ctx);

            var res = user.Login(username, password);

            Assert.IsTrue(ctx.LoggedIn);

            res = sms.SendSMS("+12223334444", "This is first message from C# API");
            res = sms.SendSMS("+12223334444", "This is second message from C# API");


        }

    }
}
