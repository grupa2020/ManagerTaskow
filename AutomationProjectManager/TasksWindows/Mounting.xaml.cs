using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy Mounting.xaml
    /// </summary>
    using ChildTasks = DataModels.TasksChildrens;
    public partial class Mounting : Window
    {
        ChildTasks.Mounting mountingTask;
        public Mounting(TaskPoco task)
        {
            InitializeComponent();
            mountingTask = new ChildTasks.Mounting(task.BoardId, task.Content, task.TaskId);
            fillContent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBoxFloat(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]+\.[0-9]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            mountingTask.UpdateContent(int.Parse(hoursBox.Text), int.Parse(workersBox.Text), float.Parse(costBox.Text), worksLstBox.Items.OfType<string>().ToList(), materialsLstBox.Items.OfType<string>().ToList());
            mountingTask.SaveTaskPUT();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(mountingTask.DeleteTask());
            this.Close();
        }

        private void fillContent()
        {
            //lstBox.Items.Clear();
            //if (maintainenceTask.Content != null && (maintainenceTask.Content.Length > 0))
            //{
            //    string[] items = maintainenceTask.Content.Split('|');
            //    foreach (string itm in items)
            //    {
            //        lstBox.Items.Add(itm);
            //    }
            //}
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaksimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            List<String> itemsToRemove = new List<String>();
            foreach (String item in worksLstBox.SelectedItems)
            {
                itemsToRemove.Add(item);
            }

            foreach (String inx in itemsToRemove)
            {
                worksLstBox.Items.Remove(inx);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (taskText.Text.Length > 0)
            {
                taskText.Text.Trim(',');
                worksLstBox.Items.Add(taskText.Text);
                taskText.Clear();
            }
        }

        private void DeleteBtn2_Click(object sender, RoutedEventArgs e)
        {
            List<String> itemsToRemove = new List<String>();
            foreach (String item in materialsLstBox.SelectedItems)
            {
                itemsToRemove.Add(item);
            }

            foreach (String inx in itemsToRemove)
            {
                materialsLstBox.Items.Remove(inx);
            }
        }

        private void AddBtn2_Click(object sender, RoutedEventArgs e)
        {
            if (taskText2.Text.Length > 0)
            {
                taskText2.Text.Trim(',');
                materialsLstBox.Items.Add(taskText2.Text);
                taskText2.Clear();
            }
        }
        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
