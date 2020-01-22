using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class DriversProject : TaskPoco
    {
        public DriversProject()
        {
            this.BoardId = -1;
            this.TaskId = -1;
            this.Type = TaskType.DriversProject;
        }

        public DriversProject(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.Type = TaskType.ProjectDescription;
        }

        public override void UpdateContent()
        {

        }
    }
}
