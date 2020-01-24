using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    class VarDefTool:TaskPoco
    {
        public VarDefTool()
        {
            this.BoardId = -1;
            this.TaskId = -1;
            this.TaskType = TaskTypeEnum.VarDefTool;
        }

        public VarDefTool(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.TaskType = TaskTypeEnum.VarDefTool;
        }

        public override void UpdateContent()
        {

        }
    }
}
