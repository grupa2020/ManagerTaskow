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


    using ChildTasks = DataModels.TasksChildrens;
    public partial class Workshop : Window
    {
        ChildTasks.Workshop workshopTask; 
        public Workshop(TaskPoco task)
        {
            InitializeComponent();
            workshopTask = new ChildTasks.Workshop(task.BoardId, task.Content, task.TaskId);
            fillContent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBoxFloat(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]+\,[0-9]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            List<String> worksTemp = worksLstBox.Items.OfType<string>().ToList();
            List<String> materialsTemp = materialsLstBox.Items.OfType<string>().ToList();

            workshopTask.UpdateContent(int.Parse(hoursBox.Text), int.Parse(workersBox.Text), float.Parse(costBox.Text), worksTemp, materialsTemp);
            workshopTask.SaveTaskPUT();
            fillContent();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(workshopTask.DeleteTask());
            this.Close();
        }

        private void fillContent()
        {
            string tempString;
            tempString = workshopTask.Content;
            if (tempString.Length > 0)
            {
                int x, y;

                //Date parsing
                x = tempString.IndexOf('|');
                y = tempString.Substring(x+1).IndexOf('|');
                textDate.Text = tempString.Substring(x+1, y);
                tempString = tempString.Substring(y+2);

                //hours parsing
                x = tempString.IndexOf('|');
                y = tempString.Substring(x + 1).IndexOf('|');
                textHours.Text = tempString.Substring(x + 1, y);
                tempString = tempString.Substring(y + 2);

                //workers count parsing
                x = tempString.IndexOf('|');
                y = tempString.Substring(x + 1).IndexOf('|');
                textWorkers.Text = tempString.Substring(x + 1, y);
                tempString = tempString.Substring(y + 2);

                //cost parsing
                x = tempString.IndexOf('|');
                y = tempString.Substring(x + 1).IndexOf('|');
                textCost.Text = tempString.Substring(x + 1, y);
                tempString = tempString.Substring(y + 2);

                //works list parsing
                x = tempString.IndexOf('|');
                y = tempString.Substring(x + 1).IndexOf('|');
                string[] items = tempString.Substring(x + 1, y).Split(';');
                foreach (string itm in items)
                {
                    worksList.Items.Add(itm);
                }
                tempString = tempString.Substring(y + 2);

                //materials list parsing
                x = tempString.IndexOf('|');
                y = tempString.Substring(x + 1).IndexOf('|');
                items = tempString.Substring(x + 1, y).Split(';');
                foreach (string itm in items)
                {
                    materialsList.Items.Add(itm);
                }
                tempString = tempString.Substring(y + 2);
            }

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
