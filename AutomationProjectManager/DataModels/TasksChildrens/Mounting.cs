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
            this.TaskType = TaskTypeEnum.Mounting;
        }

        public override void UpdateContent()
        {

        }
    }
}
