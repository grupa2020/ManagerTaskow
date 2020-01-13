using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Model
{
    class TaskPoco
    {
        public int TaskId { get; set; }
        public string Content { get; set; }
        public int BoardId { get; set; }
    }
}
