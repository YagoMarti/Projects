using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace SMSHelper.Rules
{
    public class MandrillRules
    {
        public MandrillRules()
        {
            
        }

        const string MandrillKey = "Lheu4lZERHM5jvU2Cn9Jpg";
        const string BaseUrl = "https://mandrillapp.com/api/1.0/";

        private static bool info()
        {
            var url = BaseUrl + "/api/v3/devices";
            var data = new NameValueCollection { { "email", "" }, { "password", "" }, { "page", "1" } };
            var formatedData = MyUrlHelper.ToQueryString(data);
            url += formatedData;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "GET";
            try
            {
                var response = webRequest.GetResponse();
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                if (dataStream != null)
                {
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    //return serializer.Deserialize<DeviceResponse>(responseFromServer);
                }
                //return null;
            }
            catch (Exception)
            {
                //return null;
            }
            return false;
        }


        public bool EnviarEmailconTemplate(string destinatario, string mensaje)
        {
            //List<string> toEmails = new List<string>();
            //toEmails.Add("yaguito_marti@hotmail.com");
            //var data = new NameValueCollection
            //{
            //    {"toEmails", toEmails},
            //    {"password", ""},
            //    {"number", ""},
            //    {"message", ""},
            //    {"device", ""} 
            //};
            //url en este caso opción de envio de correo
            string apiUrl = BaseUrl + "messages/send.json";

            //Lista de correos a lo cual enviar el mismo
            List<string> toEmails = new List<string>();
            toEmails.Add("yaguito_marti@hotmail.com");

            //Lista de nombres, respetar el órden de arriba con los correos
            List<string> toNames = new List<string>();
            toNames.Add("Yago Marti");

            dynamic sendParams = new System.Dynamic.ExpandoObject();
            sendParams.key = MandrillKey;
            sendParams.message = new System.Dynamic.ExpandoObject();
            sendParams.message.to = new List<dynamic>();

            //to emails
            for (int x = 0; x < toEmails.Count; x++)
            {
                sendParams.message.to.Add(new System.Dynamic.ExpandoObject());
                sendParams.message.to[x].email = toEmails[x];
            }
            //Lista de nombres, respetar el órden de arriba con los correos  
            for (int x = 0; x < toNames.Count; x++)
            {
                //Si no se agrega email, comentar la línea
                if (toEmails.Count >= x + 1)
                {
                    sendParams.message.to[x].name = toNames[x];
                }
            }

            var formatedData = MyUrlHelper.ToQueryString(data);
            apiUrl += formatedData;
            var webRequest = WebRequest.Create(apiUrl);
            webRequest.Method = "POST";
            var response = webRequest.GetResponse();
            var dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();
            if (dataStream != null) reader = new StreamReader(dataStream);
            var serializer = new JavaScriptSerializer();
            return false;
        }

        private void SendMessage()
        {
            //sendMessageToNumber
            var url = BaseUrl + "/api/v3/messages/send";
            var data = new NameValueCollection
            {
                {"email", ""},
                {"password", ""},
                {"number", ""},
                {"message", ""},
                {"device", ""} 
            };
            //Data.Add("page", "1");
            var formatedData = MyUrlHelper.ToQueryString(data);
            url += formatedData;
            var webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            var response = webRequest.GetResponse();
            var dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();
            if (dataStream != null) reader = new StreamReader(dataStream);
            var serializer = new JavaScriptSerializer();
        }
    }
}
