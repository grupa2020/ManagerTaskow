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
            this.TaskType = TaskTypeEnum.Workshop;
        }

        public override void UpdateContent()
        {

        }
    }
}
