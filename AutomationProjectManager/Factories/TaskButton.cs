using AutomationProjectManager.Model;
using SharpVectors.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AutomationProjectManager.Factories
{
    class TaskButton : Button
    {
        public int taskId { get; set; }
        string taskContent { get; set; }
        public TaskPoco Task {get;set;}

        string Snap { get; set; }
        
        public TaskButton(TaskPoco task)
        {
            this.taskContent = task.Content;
            this.Task = task;
            string taskLabel = Translate(task.TaskType);
            this.taskId = task.TaskId;
            this.Name = task.TaskType.ToString();
            
            

            Style style = this.FindResource("MaterialDesignRaisedDarkButton") as Style;   
            this.Style = style;

            this.BorderBrush = this.FindResource("GlobalBordersBrush") as Brush;

            StackPanel panelInButton = new StackPanel();
            panelInButton.Background = Brushes.Transparent;

            SvgViewbox viewbox = new SvgViewbox();
            viewbox.Source = new System.Uri("Resources/create-24px.svg", UriKind.Relative); //create-24px.svg dla algorytmu

            viewbox.Margin= new Thickness(5,5,5,5);
            viewbox.MinHeight = 30;
            viewbox.MinWidth = 30;
           
            //svgc: SvgViewbox

            TextBlock txtBox = new TextBlock()
            {
                FontSize = 14,
                FontStyle = FontStyles.Normal,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Text = taskLabel,
               // Foreground = this.FindResource("GlobalForeColor") as Brush,
                //Background = Brushes.Transparent,
                Margin= new Thickness(5, 5, 5, 5),
                Padding = new Thickness(5, 5, 5, 5)


            };

            Rectangle rectangle = new Rectangle();
            rectangle.Fill =this.FindResource("PrimaryHueMidBrush") as Brush;
            rectangle.Height = 20;
            rectangle.Margin=new Thickness(0,0,0,0);

            this.Background = this.FindResource("TextAreasBacground") as Brush;

            Snap = "Jakś zajawka";
            if(task.Content.Length<40)
            {
                Snap = task.Content;
            }
            else
            {
                Snap = task.Content.Substring(0,39);
                Snap += "...";
            }


            TextBlock txtBox2 = new TextBlock()
            {
                FontSize = 12,
               
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Text = "__________________",
                Foreground= this.FindResource("GlobalBordersBrush") as Brush,
                Background = Brushes.Transparent,
                Margin = new Thickness(5, 5, 5, 5),
            };

           // panelInButton.Children.Add(rectangle);
            panelInButton.Children.Add(viewbox);
            panelInButton.Children.Add(txtBox);            
            panelInButton.Children.Add(txtBox2);
            this.Content = panelInButton;

            panelInButton.Margin = new Thickness(0, 0, 0, 0);
            this.Margin = new Thickness(10, 10, 10, 10);
            this.MinHeight = 120;
            this.MaxHeight = 300;

            this.Padding= new Thickness(0,0,0,0);

           


            switch (task.TaskType)
            {
                case TaskTypeEnum.AlgorithmDescription:
                    {
                       txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                       
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4caf50")); //Ustawienie kolorów konkretnych typów tasków
                        txtBox2.Text =Snap;
                        
                    }
                    break;


                case TaskTypeEnum.DriversProject:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff9800"));

                       // viewbox.Source = new System.Uri("Resources/developer_mode-24px.svg", UriKind.Relative);
                    }
                    break;


                case TaskTypeEnum.ElectricalProject:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffc107")); 
                    }
                    break;
                case TaskTypeEnum.Maintainence:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fafafa"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2980b9")); 
                    }
                    break;
                case TaskTypeEnum.Mounting:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8bc34a")); 
                    }
                    break;
                case TaskTypeEnum.OrderList:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fafafa"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#16a085")); 
                    }
                    break;
                case TaskTypeEnum.ProjectDescription:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8bc34a")); 
                    }
                    break;

                case TaskTypeEnum.VarDefTool:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4caf50")); 
                    }
                    break;
                case TaskTypeEnum.Workshop:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fafafa"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c79100")); 
                    }
                    break;

                default:
                    {
                        txtBox.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                        txtBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f8c8d")); 
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
                        name = " Opis algorytmu ";
                    }
                    break;


                case TaskTypeEnum.DriversProject:
                    {
                        name = " Projekt programu sterownika ";
                    }
                    break;


                case TaskTypeEnum.ElectricalProject:
                    {
                        name = " Schemat Elektryki ";
                    }
                    break;
                case TaskTypeEnum.Maintainence:
                    {
                        name = " Zgłoszenie serwisowe ";
                    }
                    break;
                case TaskTypeEnum.Mounting:
                    {
                        name = " Prace montażowe ";
                    }
                    break;
                case TaskTypeEnum.OrderList:
                    {
                        name = " Zapotrzebowania materiałowe ";
                    }
                    break;
                case TaskTypeEnum.ProjectDescription:
                    {
                        name = " Opis projektu ";
                    }
                    break;

                case TaskTypeEnum.VarDefTool:
                    {
                        name = " Definicje zmiennych procesowych ";
                    }
                    break;
                case TaskTypeEnum.Workshop:
                    {
                        name = " Prace warsztatowe ";
                    }
                    break;

                default:
                    {
                        name = " Nieznany typ ";
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
