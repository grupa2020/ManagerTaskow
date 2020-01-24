using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels
{
    public class NewBoardPoco
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int RoleAccess { get; set; }

        public NewBoardPoco(string name, int projectId,int roleAccess)
        {
            this.Name = name;
            this.ProjectId = projectId;
            this.RoleAccess = roleAccess;
        }


    }
}
