using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels
{
    public  class NewTaskPoco    //Klasa bez pola ID slóżąca do POST
    {
        public TaskTypeEnum TaskType { get; set; }
        public int BoardId { get; set; }
        public string Content { get; set; }

        public NewTaskPoco()
        {

        }

        public NewTaskPoco(TaskTypeEnum taskType, int boardId, string content)
        {
            this.TaskType = taskType;
            this.Content = content;
            this.BoardId = boardId;

        }

    }
}
