using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels
{
    class NewProjectPoco
    {
        
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Status { get; set; }
        public int OrganizationId { get; set; }
        //public string CustomerName { get; set; }
    
        public NewProjectPoco(string name,DateTime startDate,int status, int organizationId)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.Status = status;
            this.OrganizationId = organizationId;
        }
    
    }
}
