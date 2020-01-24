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
            client.method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.serviceUri += "Boards";
                string response = client.PostMethod(this.ToNewBoardPOST());

                return response;
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
            }


            return "Something was going wrong ..";
        }

        public NewBoardPoco ToNewBoardPOST()
        {
            NewBoardPoco newBoard = new NewBoardPoco(this.Name,this.ProjectId,this.RoleAccess);
            return newBoard;
        }
    }
}
