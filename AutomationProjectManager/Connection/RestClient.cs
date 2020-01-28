using AutomationProjectManager.DataModels;
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
using System.Windows;

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
        public string ServiceUri { get; set; }
        public httpVerb Method { get; set; }
        private string AccessToken { get; set; }

        //static HttpClient clientHttp = new HttpClient();

        public RestClient()
        {
            if (Global.LoggedUser != null)
            {
                if (Global.LoggedUser.AccessToken != null)
                {
                    ServiceUri = string.Empty;
                    Method = httpVerb.NONE;
                    AccessToken = Global.LoggedUser.AccessToken;
                }
                else
                {
                   
                }
            }
            else
            {
            

            }


        }

        public string getRequest()
        {


            if (Global.LoggedUser != null)
            {
                if (Global.LoggedUser.AccessToken != null)
                {

                    string responseStr = string.Empty;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(ServiceUri);
                    httpWebRequest.Method = Method.ToString();
                    httpWebRequest.PreAuthenticate = true;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);

                    using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
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
                            responseStream.Close();
                        }
                    }


                    return responseStr;

                }
                else
                {
                    MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                    messageWindow.Show();
                }
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                messageWindow.Show();

            }

            return null;
        }

        public string PostMethod(object ObjectToSend) //Wysłanie obiektu do serwisu
        {

            if (Global.LoggedUser != null)
            {
                if (Global.LoggedUser.AccessToken != null)
                {

                    string Json = JsonConvert.SerializeObject(ObjectToSend);
                    string info = string.Empty;

                    Debug.WriteLine(Json);

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.ServiceUri);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = this.Method.ToString();
                    httpWebRequest.PreAuthenticate = true;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);



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

                    httpResponse.Close();
                    return info;

                }
                else
                {
                    MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                    messageWindow.Show();
                }
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                messageWindow.Show();

            }

            return null;

        }

        public string DeleteMethod(object ObjectToDel)   // np: "/Tasks/23  gdzie 23 to id tasku do usunięcia "
        {

            if (Global.LoggedUser != null)
            {
                if (Global.LoggedUser.AccessToken != null)
                {
                    // string Json = JsonConvert.SerializeObject(ObjectToDel);
                    string info = string.Empty;
                    //Zakomentowane na wypadek gdyby trzebabyło dawać całe body 
                    // Debug.WriteLine(Json);

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.ServiceUri);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = this.Method.ToString();
                    httpWebRequest.PreAuthenticate = true;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);

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


                    httpResponse.Close();
                    return info;

                }
                else
                {
                    MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                    messageWindow.Show();
                }
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                messageWindow.Show();

            }
            return null;
        }

        public string PutMethod(object ObjectToSend) //Wysłanie obiektu do serwisu
        {

            if (Global.LoggedUser != null)
            {
                if (Global.LoggedUser.AccessToken != null)
                {

                    string Json = JsonConvert.SerializeObject(ObjectToSend);
                    string info = string.Empty;

                    Debug.WriteLine(Json);

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.ServiceUri);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = this.Method.ToString();
                    httpWebRequest.PreAuthenticate = true;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);

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

                    httpResponse.Close();

                    return info;

                }
                else
                {
                    MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                    messageWindow.Show();
                }
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Aby poruszuszać się po systemie należy być zalogowanym :)");
                messageWindow.Show();

            }

            return null;

        }


    }
}
