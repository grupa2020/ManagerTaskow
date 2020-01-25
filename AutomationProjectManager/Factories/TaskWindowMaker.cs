using AutomationProjectManager.Model;
using AutomationProjectManager.ToolsWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutomationProjectManager.Factories
{
    class TaskWindowMaker
    {
        TaskPoco Task;
        public TaskWindowMaker(TaskPoco task)
        {
            Task = task;
        }

        public Window GetWindow()
        {
            Window newWindow;

            switch (Task.TaskType)
            {
                case TaskTypeEnum.AlgorithmDescription:
                    {
                        newWindow = new AlgorithmDescription(Task);
                        return newWindow;
                    }
                 
                case TaskTypeEnum.DriversProject:
                    {
                        newWindow = new DriversProject(Task);
                        return newWindow;

                    }
                   
                case TaskTypeEnum.ElectricalProject:
                    {
                        newWindow = new ElectricalProject(Task);
                        return newWindow;
                    }
                    
                case TaskTypeEnum.Maintainence:
                    {
                        newWindow = new Maintainence(Task);
                        return newWindow;
                    }
                    
                case TaskTypeEnum.Mounting:
                    {
                        newWindow = new Mounting(Task);
                        return newWindow;
                    }
                    
                case TaskTypeEnum.OrderList:
                    {
                        newWindow = new OrderList(Task);
                        return newWindow;
                    }
                   
                case TaskTypeEnum.ProjectDescription:
                    {
                        newWindow = new ProjectDescription(Task);
                        return newWindow;
                    }
                    

                case TaskTypeEnum.VarDefTool:
                    {
                        newWindow = new VarDefTool(Task);
                        return newWindow;
                    }
                    
                case TaskTypeEnum.Workshop:
                    {
                        newWindow = new Workshop(Task);
                        return newWindow;
                    }
                   

                default:
                    {
                        return null;
                    }
                    
            }

           
        }

    }
}
