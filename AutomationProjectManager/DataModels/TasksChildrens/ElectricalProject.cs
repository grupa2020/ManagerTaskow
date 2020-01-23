using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class ElectricalProject : TaskPoco
    {
        public ElectricalProject()
        {
            this.BoardId = -1;
            this.TaskId = -1;
            this.Type = TaskType.ElectricalProject;
        }

        public ElectricalProject(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.Type = TaskType.ElectricalProject;
        }

        public override void UpdateContent()
        {

        }
    }
}
