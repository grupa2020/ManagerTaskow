using AutomationProjectManager.DataModels.TasksChildrens;
using AutomationProjectManager.Model;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy OrderList.xaml
    /// </summary>
    /// 
    using ChildTasks = DataModels.TasksChildrens;
    public partial class OrderList : Window
    {
        ChildTasks.OrderList orderListTask;
        
        public OrderList(TaskPoco task)
        {
            InitializeComponent();
            orderListTask = new ChildTasks.OrderList(task.BoardId, task.Content, task.TaskId);
            fillContent();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            orderListTask.Content = GetListsAsString();
            orderListTask.SaveTaskPUT();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(orderListTask.DeleteTask());
            this.Close();
        }

        private String GetListsAsString()
        {
            foreach(String str in lstBox.Items)
            {
                str.Trim('|');
            }
            List<string> tasks = new List<string>();
            foreach (string str in lstBox.Items)
            {
                tasks.Add(str);
            }

            return string.Join("|", tasks);
        }

        private void fillContent()
        {
            lstBox.Items.Clear();
            if (orderListTask.Content != null && (orderListTask.Content.Length > 0))
            {
                string[] items = orderListTask.Content.Split('|');
                foreach (string itm in items)
                {
                    lstBox.Items.Add(itm);
                }
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
            foreach (String item in lstBox.SelectedItems)
            {
                itemsToRemove.Add(item);
            }

            foreach(String inx in itemsToRemove)
            {
                lstBox.Items.Remove(inx);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (orderText.Text.Length > 0)
            {
                orderText.Text.Trim('|');
                lstBox.Items.Add(orderText.Text);
                orderText.Clear();
            }
        }
    }
}
