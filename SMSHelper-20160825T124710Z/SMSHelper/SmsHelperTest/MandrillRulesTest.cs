using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMSHelper.Rules;

namespace SmsHelperTest
{
    [TestClass]
    public class MandrillRulesTest
    {
        private const string User = "info@webmovin.com.au";
        private const string Pass = "Webmovin123";
        //private const string User = "martin.posada@la.com";
        //private const string Pass = "123456";
        //readonly Credentials _credentials = new Credentials(User,Pass);
        readonly MandrillRules _MandrillRules = new MandrillRules();

        [TestMethod]
        public void TestEnviarMensaje()
        {
            _MandrillRules.EnviarEmailconTemplate("yaguito_marti@hotmail.com", "<h3> Bienvenido ! </h3>");
            //_smsRules.EnviarMensaje(_credentials, "+61490143327",
            //    "Hi, There is a Delivery Job Quote for you with WEBMOVIN.COM.AU " +
            //    "To see pricing & continue booking: webmovin.com.au/Booking.aspx?token=3rc9A5ZN35 + Regards");
        }

    }
}
