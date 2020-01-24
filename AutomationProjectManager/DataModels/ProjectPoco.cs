using AutomationProjectManager.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Model
{
    class ProjectPoco
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Status { get; set; }
        public int OrganizationId { get; set; }
        //public string CustomerName { get; set; }


        public ProjectPoco(string name, DateTime startTime,int status, int orgId,string customer,int projectId)
        {
            ProjectId = projectId;
            Name = name;
            StartDate = startTime;
            Status = status;
            OrganizationId = orgId;
          //  CustomerName = customer;
            
        }

        public string SaveProjectPOST()
        {
            RestClient client = new RestClient();
            client.method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.serviceUri += "Projects";
                string response = client.PostMethod(this.ToNewProjectPOST());

                return response;
            }
            catch(Exception e)
            {
                Debug.Write(e.ToString());
            }


            return "Something was going wrong .." ;
        }

        public NewProjectPoco ToNewProjectPOST()
        {
            NewProjectPoco newProject = new NewProjectPoco(this.Name, this.StartDate, this.Status, this.OrganizationId);
            return newProject;            
        }
  
    }
}
