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

namespace AutomationProjectManager
{
    /// <summary>
    /// Logika interakcji dla klasy AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        int BoardId;
        public event EventHandler AddTaskWnd_Closing;
        public AddTaskWindow(int boardId)
        {
            InitializeComponent();
            BoardId = boardId;
            fillComboBox();
        }

        public void fillComboBox()
        {
            foreach(TaskTypeEnum taskType in Enum.GetValues(typeof(TaskTypeEnum)))
            {
                selectTypeCmbox.Items.Add(taskType);
            }
        }
        private void saveTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if(selectTypeCmbox.SelectedItem!=null)
            {
                TaskPoco newTask = new TaskPoco();
                newTask.BoardId = BoardId;
                newTask.Content = string.Empty;
                newTask.TaskId = 0;
                newTask.TaskType = (TaskTypeEnum)selectTypeCmbox.SelectedItem;
                newTask.UpdateContent();
                newTask.SaveTaskPOST();

                this.Close();
                
            }
            else
            {
                MessageBox.Show("Proszę wybrać typ zadania :)");
            }
            

           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AddTaskWnd_Closing(this, EventArgs.Empty);  //Wywołanie zdarzenia informującego o zamykaniu w oknie właścicielu okna;
        }
    }
}
