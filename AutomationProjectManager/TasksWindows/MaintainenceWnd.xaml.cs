using AutomationProjectManager.DataModels.TasksChildrens;
using AutomationProjectManager.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
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
    /// Logika interakcji dla klasy Maintainence.xaml
    /// </summary>
    using ChildTasks = DataModels.TasksChildrens;
    public partial class MaintainenceWnd : Window
    {
        Maintainence maintainenceTask;

        TaskPoco mainTask;

        public MaintainenceWnd(TaskPoco task)
        {
            InitializeComponent();
            mainTask = task;
            maintainenceTask = new ChildTasks.Maintainence(mainTask.Content);
            fillContent();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            updateContent();
            mainTask.SaveTaskPUT();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow("Czy na pewno chcesz usunąć to zadanie? ", true);

            if(message.ShowDialog()==true)
            {
                materialsLstBox.ItemsSource = null;
               MessageWindow message2=new MessageWindow (mainTask.DeleteTask());
                message2.Show();
                this.Close();
            }

           


        }

    

        private void updateContent()
        {
            try
            {
                maintainenceTask.date = datePicker.SelectedDate.Value;
                maintainenceTask.Description = this.Description.Text;
                maintainenceTask.People = this.People.Text;
                mainTask.Content = JsonConvert.SerializeObject(maintainenceTask);
                mainTask.SaveTaskPUT();
            }
            catch (Exception e)
            {
                MessageWindow message = new MessageWindow(e.Message);
            }
        }


        private void fillContent()
        {
            try
            {
                this.Description.Text = maintainenceTask.Description;
                this.People.Text = maintainenceTask.People;
                this.materialsLstBox.ItemsSource = maintainenceTask.Materials;
                this.datePicker.SelectedDate = maintainenceTask.date;
            }
            catch (Exception e)
            {
                MessageWindow message = new MessageWindow(e.Message);
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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                materialsLstBox.ItemsSource = null;
                maintainenceTask.Materials.Add(newMaterialTxtBox.Text);
                materialsLstBox.ItemsSource = maintainenceTask.Materials;

            }
            catch (Exception exception)
            {
                MessageWindow message = new MessageWindow(exception.Message);
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow("Czy na pewno chcesz usunąć wybrany materiał z listy? \n" + materialsLstBox.SelectedItem,true);

            if(message.ShowDialog()==true)
            {
                int selectedIndex = materialsLstBox.SelectedIndex;
                materialsLstBox.ItemsSource = null;
                maintainenceTask.Materials.RemoveAt(selectedIndex);
                materialsLstBox.ItemsSource = maintainenceTask.Materials;
            }
            else
            {
                //Nic nie rób
            }


        }


        //Tylko do testów
        private void materialsLstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // MessageWindow message = new MessageWindow(materialsLstBox.SelectedIndex.ToString());
           // message.Show();
        }
    }
}