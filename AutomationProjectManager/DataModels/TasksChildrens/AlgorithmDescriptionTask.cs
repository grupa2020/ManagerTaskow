using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class AlgorithmDescriptionTask:TaskPoco
    {
        string Description { get; set; }


        public AlgorithmDescriptionTask()
        {
            this.BoardId = -1;
            this.Description = "";
            this.TaskId = -1;
            this.Type = TaskType.AlgorithmDescription;
        }

        public AlgorithmDescriptionTask(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.Description = Content;
            this.TaskId = TaskId;
            this.Type = TaskType.AlgorithmDescription;
        }

        public override void UpdateContent()
        {
            this.Content = Description;
        }


    }
}
