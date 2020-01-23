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
        int[] itemsCountInColumn { get; set; }  //przechowuje ile tasków konkretnego typu już jest w gridzie
        public TaskBoardXY()
        {
            itemsCountInColumn = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0,0 };
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
                        itemsCountInColumn[2]++;
                        return itemsCountInColumn[2];
                    }

                case TaskType.Maintainence:
                    {
                        itemsCountInColumn[3]++;
                        return itemsCountInColumn[3];
                    }

                case TaskType.Mounting:
                    {
                        itemsCountInColumn[4]++;
                        return itemsCountInColumn[4];
                    }

                case TaskType.OrderList:
                    {
                        itemsCountInColumn[5]++;
                        return itemsCountInColumn[5];
                    }

                case TaskType.ProjectDescription:
                    {
                        itemsCountInColumn[6]++;
                        return itemsCountInColumn[6];
                    }


                case TaskType.VarDefTool:
                    {
                        itemsCountInColumn[7]++;
                        return itemsCountInColumn[7];
                    }

                case TaskType.Workshop:
                    {
                        itemsCountInColumn[8]++;
                        return itemsCountInColumn[8];
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
    }
}
