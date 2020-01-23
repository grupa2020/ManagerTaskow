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
      
        public TaskButton(TaskPoco task)
        {
            string taskLabel = Translate(task.Type);
            this.taskId = task.TaskId;
            this.Name = task.Type.ToString();
            this.Content = new TextBlock()
            {
                FontSize = 14,
                FontStyle = FontStyles.Normal,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Text = taskLabel
            };


            Style roundedCorners = new Style(typeof(Border));
            roundedCorners.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(12)));

            this.Resources.Add(typeof(Border), roundedCorners);

            
            //this.Resources.Add()
           
            this.Margin = new Thickness(10, 10, 10, 10);
            //this.Width = 300;
            //this.Height = 200;
            this.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));

            switch (task.Type)
            {
                case TaskType.AlgorithmDescription:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f39c12")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;


                case TaskType.DriversProject:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d35400")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;


                case TaskType.ElectricalProject:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8e44ad")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;
                case TaskType.Maintainence:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2980b9")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;
                case TaskType.Mounting:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#27ae60")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;
                case TaskType.OrderList:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#16a085")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;
                case TaskType.ProjectDescription:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2c3e50")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;

                case TaskType.VarDefTool:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("##34495e")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;
                case TaskType.Workshop:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c0392b")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;

                default:
                    {
                        this.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ecf0f1"));
                        this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f8c8d")); //Tutaj też dodać switch case tak żeby kolo był inny dla każdego tasku
                    }
                    break;
            }


        }

        public string Translate(TaskType type)
        {
            string name="";

            switch (type)
            {
                case TaskType.AlgorithmDescription:
                    {
                        name = "Opis algorytmu";
                    }
                    break;


                case TaskType.DriversProject:
                    {
                        name = "Projekt programu sterownika";
                    }
                    break;


                case TaskType.ElectricalProject:
                    {
                        name = "Schemat Elektryki";
                    }
                    break;
                case TaskType.Maintainence:
                    {
                        name = "Zgłoszenie serwisowe";
                    }
                    break;
                case TaskType.Mounting:
                    {
                        name = "Prace montażowe";
                    }
                    break;
                case TaskType.OrderList:
                    {
                        name = "Zapotrzebowania materiałowe";
                    }
                    break;
                case TaskType.ProjectDescription:
                    {
                        name = "Opis projektu";
                    }
                    break;

                case TaskType.VarDefTool:
                    {
                        name = "Definicje zmiennych procesowych";
                    }
                    break;
                case TaskType.Workshop:
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

    }
}
