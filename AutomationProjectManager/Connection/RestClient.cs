using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

            using(HttpWebResponse response =(HttpWebResponse)request.GetResponse())
            {
                if(response.StatusCode !=HttpStatusCode.OK)
                {
                    throw new ApplicationException("Nie uzyskano odpowiedzi od serwera REST, error code:"+response.StatusCode.ToString());
                }

                using(Stream responseStream =response.GetResponseStream())
                {
                    using(StreamReader reader =new StreamReader(responseStream))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                }
            }

            return responseStr;
        }
        //public bool Autenticate(UsersPoco);
    }
}
