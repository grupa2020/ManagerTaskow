﻿using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class Maintainence : TaskPoco
    {
        public Maintainence()
        {
            this.BoardId = -1;
            this.TaskId = -1;
            this.Type = TaskType.Maintainence;
        }

        public Maintainence(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.Type = TaskType.Maintainence;
        }

        public override void UpdateContent()
        {

        }
    }
}