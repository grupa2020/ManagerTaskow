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
            string info = string.Empty;

            Debug.WriteLine(Json);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.serviceUri);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = this.method.ToString();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                info = result.ToString();
            }



            return info;

        }

        public string DeleteMethod(object ObjectToDel)   // np: "/Tasks/23  gdzie 23 to id tasku do usunięcia "
        {
           // string Json = JsonConvert.SerializeObject(ObjectToDel);
            string info = string.Empty;
                                                                //Zakomentowane na wypadek gdyby trzebabyło dawać całe body 
           // Debug.WriteLine(Json);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.serviceUri);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = this.method.ToString();

            httpWebRequest.GetRequestStream();

            /*
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Json);
            }
            */

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                info = result.ToString();
            }

    

            return info;
        }



    }
}
