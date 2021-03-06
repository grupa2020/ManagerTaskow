﻿using AutomationProjectManager.Model;
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
            this.TaskType = TaskTypeEnum.DriversProject;
        }
        public DriversProject(TaskPoco task)
        {
            this.BoardId = task.BoardId;
            this.TaskId = task.TaskId;
            this.Content = task.Content;
            this.TaskType = TaskTypeEnum.DriversProject;
        }
        public DriversProject(int BoardId, string Content, int TaskId)
        {
            this.BoardId = BoardId;
            this.TaskId = TaskId;
            this.TaskType = TaskTypeEnum.DriversProject;
        }

        new public  void UpdateContent()
        {

        }
    }
}
