using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutomationProjectManager.Factories
{
    class TaskButton : Button
    {
        public int taskId { get; set; }
        string taskContent { get; set;}

        public TaskButton(TaskPoco task)
        {
            this.taskContent = task.Content;

            string taskLabel = Translate(task.TaskType);
            this.taskId = task.TaskId;
            this.Name = task.TaskType.ToString();
            this.Content = new TextBlock()
            {
                FontSize = 14,
                FontStyle = FontStyles.Normal,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Text = taskLabel
            };

            this.MinHeight = 50;
            
            this.MaxHeight = 50;

            Style roundedCorners = new Style(typeof(Border));
            roundedCorners.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(12)));

            this.Resources.Add(typeof(Border), roundedCorners);

            
            //this.Resources.Add()
           
            this.Margin = new Thickness(10, 10, 10, 10);
            //this.Width = 300;
            //this.Height = 200;
            this.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));

            switch (task.TaskType)
            {
                case TaskTypeEnum.AlgorithmDescription:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f39c12")); //Ustawienie kolorów konkretnych typów tasków
                    }
                    break;


                case TaskTypeEnum.DriversProject:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d35400")); 
                    }
                    break;


                case TaskTypeEnum.ElectricalProject:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8e44ad")); 
                    }
                    break;
                case TaskTypeEnum.Maintainence:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2980b9")); 
                    }
                    break;
                case TaskTypeEnum.Mounting:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#27ae60")); 
                    }
                    break;
                case TaskTypeEnum.OrderList:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#16a085")); 
                    }
                    break;
                case TaskTypeEnum.ProjectDescription:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2c3e50")); 
                    }
                    break;

                case TaskTypeEnum.VarDefTool:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#34495e")); 
                    }
                    break;
                case TaskTypeEnum.Workshop:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c0392b")); 
                    }
                    break;

                default:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f8c8d")); 
                    }
                    break;
            }


        }

        public string Translate(TaskTypeEnum type)
        {
            string name="";

            switch (type)
            {
                case TaskTypeEnum.AlgorithmDescription:
                    {
                        name = "Opis algorytmu";
                    }
                    break;


                case TaskTypeEnum.DriversProject:
                    {
                        name = "Projekt programu sterownika";
                    }
                    break;


                case TaskTypeEnum.ElectricalProject:
                    {
                        name = "Schemat Elektryki";
                    }
                    break;
                case TaskTypeEnum.Maintainence:
                    {
                        name = "Zgłoszenie serwisowe";
                    }
                    break;
                case TaskTypeEnum.Mounting:
                    {
                        name = "Prace montażowe";
                    }
                    break;
                case TaskTypeEnum.OrderList:
                    {
                        name = "Zapotrzebowania materiałowe";
                    }
                    break;
                case TaskTypeEnum.ProjectDescription:
                    {
                        name = "Opis projektu";
                    }
                    break;

                case TaskTypeEnum.VarDefTool:
                    {
                        name = "Definicje zmiennych procesowych";
                    }
                    break;
                case TaskTypeEnum.Workshop:
                    {
                        name = "Prace warsztatowe";
                    }
                    break;

                default:
                    {
                        name = "Nieznany typ";
                    }
                    break;
            }


            return name;
        }


        public string GetContent()
        {
            return this.taskContent;
        }

    }
}
