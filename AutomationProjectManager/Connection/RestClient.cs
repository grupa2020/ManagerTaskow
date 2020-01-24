using AutomationProjectManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace AutomationProjectManager
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE,
        NONE

    }
    class RestClient
    {
        public string serviceUri { get; set; }
        public httpVerb method { get; set; }

        static HttpClient clientHttp = new HttpClient();

        public RestClient()
        {
            serviceUri = string.Empty;
            method = httpVerb.NONE;
        }

        public string getRequest()
        {
            string responseStr = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUri);
            request.Method = method.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Nie uzyskano odpowiedzi od serwera REST, error code:" + response.StatusCode.ToString());
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                }
            }

            return responseStr;
        }

        public string PostMethod(object ObjectToSend) //Wysłanie obiektu do serwisu
        {


            string Json = JsonConvert.SerializeObject(ObjectToSend);

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(Json);

            HttpWebRequest request = WebRequest.Create(this.serviceUri) as HttpWebRequest;


            request.Method = this.method.ToString();
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response.StatusDescription;

        }



    }
}
