using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Model
{
   public enum TaskType {AlgorithmDescription, ProjectDescription, DriversProject,ElectricalProject,
        Maintainence,Mounting,OrderList,VarDefTool,Workshop };//Typy tasków w folderze TasksChildrens są klasy konkretnych typów które dziedziczą po klasie TaskPoco
    public class TaskPoco
    {
        public int TaskId { get; set; }
        public TaskType Type { get; set; }
        public int BoardId { get; set; }
        public string Content { get; set; }
        public virtual void UpdateContent()
        {
            Content = "";
        }
    }
}
