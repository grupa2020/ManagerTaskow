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
   public enum TaskTypeEnum {AlgorithmDescription, ProjectDescription, DriversProject,ElectricalProject,
        Maintainence,Mounting,OrderList,VarDefTool,Workshop };//Typy tasków w folderze TasksChildrens są klasy konkretnych typów które dziedziczą po klasie TaskPoco
    public class TaskPoco
    {
        public int TaskId { get; set; }
        public TaskTypeEnum TaskType { get; set; }
        public int BoardId { get; set; }
        public string Content { get; set; }
      

        public void UpdateContent()
        {
            Content = "";
        }
      
        public string SaveTaskPOST()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Tasks";

                string response = client.PostMethod(this.TotaskToPOST());

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


        public NewTaskPoco TotaskToPOST()   //KONWERSJA NA TASK BEZ ID AKCEPTOWANY PRZRZ SERWIS
        {
            NewTaskPoco newTask = new NewTaskPoco(this.TaskType, this.BoardId, this.Content);

            return newTask;
        }

        public TaskPoco ToTaskPoco()
        {
            TaskPoco newPoco = new TaskPoco();
            newPoco.BoardId = this.BoardId;
            newPoco.Content = this.Content;
            newPoco.TaskId = this.TaskId;
            newPoco.TaskType = this.TaskType;

            return newPoco;
        }

        public string SaveTaskPUT()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.PUT;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Tasks";

                string response = client.PutMethod(this.ToTaskPoco());

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

        public string DeleteTask()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.DELETE;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.ServiceUri += "Tasks/" + this.TaskId.ToString();

            return client.DeleteMethod(this);
        }
    }
}
