using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class ProjectDescription:TaskPoco
    {
        public string Description { get; set; }
        public ProjectDescription()
        {
            this.BoardId = -1;
            this.Description = "";
            this.TaskId = -1;
            this.TaskType = TaskTypeEnum.ProjectDescription;
        }

        public ProjectDescription(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.Description = Content;
            this.TaskId = TaskId;
            this.TaskType = TaskTypeEnum.ProjectDescription;
        }

        new public  void UpdateContent()
        {
            this.Content = Description;
        }
    }
}
