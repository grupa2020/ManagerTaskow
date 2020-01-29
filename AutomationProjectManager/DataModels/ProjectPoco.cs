using AutomationProjectManager.Connection.Responses;
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
    public class ProjectPoco
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Status { get; set; }
        public int OrganizationId { get; set; }
        public string CustomerName { get; set; }


        public ProjectPoco(string name, DateTime startTime,int status, int orgId,string customer,int projectId)
        {
            ProjectId = projectId;
            Name = name;
            StartDate = startTime;
            Status = status;
            OrganizationId = orgId;
            CustomerName = customer;
            
        }

        public string SaveProjectPOST()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Projects";
                string response = client.PostMethod(this.ToNewProjectPOST());

                return response;
            }
            catch(Exception e)
            {
                Debug.Write(e.ToString());
            }


            SimpleResponse serverBad = new SimpleResponse();
            serverBad.Succeeded = false;
            serverBad.Message = "Serwer nie odpowiedział...";
            return JsonConvert.SerializeObject(serverBad);
        }

        public NewProjectPoco ToNewProjectPOST()
        {
            NewProjectPoco newProject = new NewProjectPoco(this.Name, this.StartDate, this.Status, this.OrganizationId,this.CustomerName);
            return newProject;            
        }

        public string ProjectDELETE()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.DELETE;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.ServiceUri += "Projects/" + this.ProjectId.ToString();

            return(client.DeleteMethod(this));

        }

        public string SaveProjectPUT()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.PUT;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Projects";
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

    }
}
