using AutomationProjectManager.Connection.Responses;
using AutomationProjectManager.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Model
{
    class UsersPoco
    {
            public int UserId { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public int Role { get; set; }
            public int OrganizationId { get; set; }

        public UsersPoco(int userId, string login, string password, string name, int role, int organizationId)
        {
            UserId = userId;
            Login = login;
            Password = password;
            Name = name;
            Role = role;
            OrganizationId = organizationId;
        }

        public string AddUserPOST()
        {
            RestClient client = new RestClient();
            client.method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.serviceUri += "Users";

                string response = client.PostMethod(this.ToUserToPOST());

                return response;
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
            }

            SimpleResponse serverBad = new SimpleResponse();
            serverBad.Succeeded = false;
            serverBad.Message = "Serwer nie odpowiedział...";
            return JsonConvert.SerializeObject(serverBad);
        }


        public NewUsersPoco ToUserToPOST()   //KONWERSJA NA UsersPoco BEZ ID AKCEPTOWANY PRZRZ SERWIS
        {
            NewUsersPoco newUser =new NewUsersPoco(this.Login,this.Password,this.Name,this.Role,this.OrganizationId);

            return newUser;
        }


        public string SaveUserPUT()
        {
            RestClient client = new RestClient();
            client.method = httpVerb.PUT;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.serviceUri += "Users";

                string response = client.PutMethod(this);

                return response;
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
            }

            SimpleResponse serverBad = new SimpleResponse();
            serverBad.Succeeded = false;
            serverBad.Message = "Serwer nie odpowiedział...";
            return JsonConvert.SerializeObject(serverBad);
        }

        public string DeleteUser()
        {
            RestClient client = new RestClient();
            client.method = httpVerb.DELETE;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.serviceUri += "Users/" + this.UserId.ToString();

            return client.DeleteMethod(this);
        }
    }
}
