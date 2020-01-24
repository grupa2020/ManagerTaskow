using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Factories
{
    class TaskBoardXY
    {
        public int[] itemsCountInColumn { get; set; }  //przechowuje ile tasków konkretnego typu już jest w gridzie
        public TaskBoardXY()
        {
            itemsCountInColumn = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
        }
        public int GetDestinationRow(TaskType taskType)
        {
            

            switch (taskType)
            {
                case TaskType.AlgorithmDescription:
                    {
                        itemsCountInColumn[0]++;
                        return itemsCountInColumn[0];
                    }


                case TaskType.DriversProject:
                    {
                        itemsCountInColumn[1]++;
                        return itemsCountInColumn[1];
                    }



                case TaskType.ElectricalProject:
                    {
                        itemsCountInColumn[1]++;
                        return itemsCountInColumn[1];
                    }

                case TaskType.Maintainence:
                    {
                        itemsCountInColumn[5]++;
                        return itemsCountInColumn[5];
                    }

                case TaskType.Mounting:
                    {
                        itemsCountInColumn[4]++;
                        return itemsCountInColumn[4];
                    }

                case TaskType.OrderList:
                    {
                        itemsCountInColumn[2]++;
                        return itemsCountInColumn[2];
                    }

                case TaskType.ProjectDescription:
                    {
                        itemsCountInColumn[0]++;
                        return itemsCountInColumn[0];
                    }


                case TaskType.VarDefTool:
                    {
                        itemsCountInColumn[1]++;
                        return itemsCountInColumn[1];
                    }

                case TaskType.Workshop:
                    {
                        itemsCountInColumn[3]++;
                        return itemsCountInColumn[3];
                    }


                default:
                    {
                        return 0;
                    }

            }



        }

        public int GetMax()
        {
            int max=0;

            for(int i=0;i<this.itemsCountInColumn.Length;i++)
            {
                if(max<itemsCountInColumn[i])
                {
                    max = itemsCountInColumn[i];
                }
            }

            return max;
            
        }

        public int GetColumn(TaskType type)
        {
            

            if (type==TaskType.AlgorithmDescription || type==TaskType.ProjectDescription)
            {
                return 0;
            }

            if (type == TaskType.DriversProject || type == TaskType.ElectricalProject || type == TaskType.VarDefTool)
            {
                return 1;
            }

            if (type == TaskType.OrderList)
            {
                return 2;
            }

            if (type == TaskType.Workshop)
            {
                return 3;
            }

            if (type == TaskType.Mounting)
            {
                return 4;
            }

            if (type == TaskType.Maintainence)
            {
                return 5;
            }

            return 0;
        }
    }
}
