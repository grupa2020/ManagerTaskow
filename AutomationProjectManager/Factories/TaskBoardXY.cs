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
        public int GetDestinationRow(TaskTypeEnum taskType)
        {
            

            switch (taskType)
            {
                case TaskTypeEnum.AlgorithmDescription:
                    {
                        itemsCountInColumn[0]++;
                        return itemsCountInColumn[0];
                    }


                case TaskTypeEnum.DriversProject:
                    {
                        itemsCountInColumn[1]++;
                        return itemsCountInColumn[1];
                    }



                case TaskTypeEnum.ElectricalProject:
                    {
                        itemsCountInColumn[1]++;
                        return itemsCountInColumn[1];
                    }

                case TaskTypeEnum.Maintainence:
                    {
                        itemsCountInColumn[5]++;
                        return itemsCountInColumn[5];
                    }

                case TaskTypeEnum.Mounting:
                    {
                        itemsCountInColumn[4]++;
                        return itemsCountInColumn[4];
                    }

                case TaskTypeEnum.OrderList:
                    {
                        itemsCountInColumn[2]++;
                        return itemsCountInColumn[2];
                    }

                case TaskTypeEnum.ProjectDescription:
                    {
                        itemsCountInColumn[0]++;
                        return itemsCountInColumn[0];
                    }


                case TaskTypeEnum.VarDefTool:
                    {
                        itemsCountInColumn[1]++;
                        return itemsCountInColumn[1];
                    }

                case TaskTypeEnum.Workshop:
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

        public int GetColumn(TaskTypeEnum type)
        {
            

            if (type==TaskTypeEnum.AlgorithmDescription || type==TaskTypeEnum.ProjectDescription)
            {
                return 0;
            }

            if (type == TaskTypeEnum.DriversProject || type == TaskTypeEnum.ElectricalProject || type == TaskTypeEnum.VarDefTool)
            {
                return 1;
            }

            if (type == TaskTypeEnum.OrderList)
            {
                return 2;
            }

            if (type == TaskTypeEnum.Workshop)
            {
                return 3;
            }

            if (type == TaskTypeEnum.Mounting)
            {
                return 4;
            }

            if (type == TaskTypeEnum.Maintainence)
            {
                return 5;
            }

            return 0;
        }
    }
}
