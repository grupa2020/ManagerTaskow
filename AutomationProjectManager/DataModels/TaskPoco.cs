using AutomationProjectManager.DataModels;
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
        public virtual void UpdateContent()
        {
            Content = "";
        }

      


        public string SaveTaskPOST()
        {
            RestClient client = new RestClient();
            client.method = httpVerb.POST;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.serviceUri += "Tasks";

                string response = client.PostMethod(this.TotaskToPOST());

                return response;
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
            }


            return "Something was going wrong ..";
        }


        public NewTaskPoco TotaskToPOST()   //KONWERSJA NA TASK BEZ ID AKCEPTOWANY PRZRZ SERWIS
        {
            NewTaskPoco newTask = new NewTaskPoco(this.TaskType, this.BoardId, this.Content);

            return newTask;
        }
    }
}
