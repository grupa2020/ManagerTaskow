using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class Mounting : TaskPoco
    {

        int hours;
        int workersCount;
        float cost;
        List<String> works;
        List<String> materials;
        public Mounting()
        {
            this.BoardId = -1;
            this.TaskId = -1;
            this.TaskType = TaskTypeEnum.Mounting;
        }

        public Mounting(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.Content = Content;
            this.TaskType = TaskTypeEnum.Mounting;
            hours = 0;
            workersCount = 0;
            cost = 0;
            works = new List<string>();
            materials = new List<string>();
        }

        public  void UpdateContent(int Hours, int WorkersCount, float Cost, List<String> Works, List<String> Materials)
        {
            hours = Hours;
            workersCount = WorkersCount;
            cost = Cost;
            works = new List<string>(Works);
            materials = new List<string>(Materials);

            string tempContent = "";

            tempContent += "h|" + hours.ToString()+"|";
            tempContent += "w|" + workersCount.ToString() + "|";
            tempContent += "c|" + cost.ToString() + "|";
            tempContent += "Lw|";
            tempContent += string.Join(";", works);
            tempContent += "|";
            tempContent += "Lm|";
            tempContent += string.Join(";", materials);
            tempContent += "|";

            Content = tempContent;
        }
    }
}
