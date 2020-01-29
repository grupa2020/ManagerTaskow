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
using System.Web.Script.Serialization;

namespace AutomationProjectManager.Model
{
     class BoardPoco
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int RoleAccess { get; set; }


        public BoardPoco(string name,int projectId, int roleAccess)
        {
            this.Name = name;
            this.ProjectId = projectId;
            this.RoleAccess = roleAccess;
        }

        public string SaveBoardPOST()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Boards";
                string response = client.PostMethod(this.ToNewBoardPOST());

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

        public NewBoardPoco ToNewBoardPOST()
        {
            NewBoardPoco newBoard = new NewBoardPoco(this.Name,this.ProjectId,this.RoleAccess);
            return newBoard;
        }

        public string SaveBoardPUT()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.PUT;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Boards";
                string response = client.PostMethod(this);

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
    }
}
