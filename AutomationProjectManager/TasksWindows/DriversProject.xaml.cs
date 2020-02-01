using AutomationProjectManager.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutomationProjectManager.ToolsWindows
{
    /// <summary>
    /// Logika interakcji dla klasy DriversProject.xaml
    /// </summary>
    using ChildTasks = AutomationProjectManager.DataModels.TasksChildrens;

    public partial class DriversProject : Window
    {
        ChildTasks.DriversProject driversTask;
        public DriversProject(TaskPoco task)
        {
            InitializeComponent();
            driversTask = new ChildTasks.DriversProject(task);
            fillContent();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            driversTask.Content = PathText.Text;
            driversTask.SaveTaskPUT(); 
        }

        private void fillContent()
        {
            PathText.Text = driversTask.Content;
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaksimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState==WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
            
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                PathText.Text = openFileDialog.FileName;
        }
    }
}
