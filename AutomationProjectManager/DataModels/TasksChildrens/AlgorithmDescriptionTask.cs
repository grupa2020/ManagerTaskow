using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class AlgorithmDescriptionTask:TaskPoco
    {
       public string Description { get; set; }


        public AlgorithmDescriptionTask()
        {
            this.BoardId = -1;
            this.Description = "";
            this.TaskId = -1;
            this.TaskType = TaskTypeEnum.AlgorithmDescription;
        }

        public AlgorithmDescriptionTask(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.Description = Content;
            this.TaskId = TaskId;
            this.TaskType = TaskTypeEnum.AlgorithmDescription;
        }

        new public  void UpdateContent()
        {
            this.Content = Description;
        }
     

    }
}
