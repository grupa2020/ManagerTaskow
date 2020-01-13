using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Model
{
    class UsersPoco
    {
            public int UserId { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public int Role { get; set; }
            public string OrganizationId { get; set; }
    }
}
