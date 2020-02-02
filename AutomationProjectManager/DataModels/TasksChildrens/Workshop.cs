using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class Workshop : TaskPoco
    {
        int hours;
        int workersCount;
        float cost;
        List<String> works;
        List<String> materials;
        public Workshop()
        {
            this.BoardId = -1;
            this.TaskId = -1;
            this.TaskType = TaskTypeEnum.Workshop;
        }

        public Workshop(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.Content = Content;
            this.TaskType = TaskTypeEnum.Workshop;
        }

        new public void UpdateContent(int Hours, int WorkersCount, float Cost, List<String> Works, List<String> Materials)
        {
            hours = Hours;
            workersCount = WorkersCount;
            cost = Cost;
            works = new List<string>(Works);
            materials = new List<string>(Materials);

            string tempContent = "";

            tempContent += '|' + DateTime.Now.ToString().Substring(0, 10) + '|';
            tempContent += '|' + hours.ToString() + '|';
            tempContent += '|' + workersCount.ToString() + '|';
            tempContent += '|' + cost.ToString() + '|';
            tempContent += '|';
            tempContent += string.Join(";", works);
            tempContent += '|';
            tempContent += '|';
            tempContent += string.Join(";", materials);
            tempContent += '|';

            Content = tempContent;
        }
    }
}
