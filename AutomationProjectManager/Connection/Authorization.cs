using AutomationProjectManager.Connection.Responses;
using AutomationProjectManager.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Connection
{

    class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Authorization(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public void GetAccess()
        {
            string ServiceUri = null;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }

            ServiceUri += "login";   // TU DODAĆ ADRESS ROUTEA !!

            if(this.Login!=null && this.Password!=null )        //Może sprawdzanie połączenia?
            {
                string Json = JsonConvert.SerializeObject(this);
                string info = string.Empty;
                
                Debug.WriteLine(Json);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ServiceUri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                
                

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

                if(info!=null)  //Zapis tokena -Deserializacja
                {

                    var response = new ValueResponse<TokenType>(true, string.Empty, null);
                    response= JsonConvert.DeserializeObject<ValueResponse<TokenType>>(info);
                    
                    if (response.Succeeded)
                    {
                        Global.LoggedUser.AccessToken = response.Value.AccessToken;
                        MessageWindow message = new MessageWindow(response.Message);
                        message.Show();

                    }
                    else
                    {
                        MessageWindow message = new MessageWindow(response.Message);
                        message.Show();
                    }
                                                         
                }

                httpResponse.Close();
            }
           

        }

        public void Logout()
        {
            Global.LoggedUser.AccessToken = null;
        }
    }
}
