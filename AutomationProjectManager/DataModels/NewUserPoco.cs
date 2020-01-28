using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels
{
    public class NewUsersPoco
    {

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public int OrganizationId { get; set; }

        public NewUsersPoco(string login,string password, string name,int role,int organizationId)
        {
            Login = login;
            Password = password;
            Name = name;
            Role = role;
            OrganizationId = organizationId;
        }
    }
}
