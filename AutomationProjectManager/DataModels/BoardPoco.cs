using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AutomationProjectManager.Model
{
    class BoardPoco
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int RoleAccess { get; set; }
    
    
    }

}
