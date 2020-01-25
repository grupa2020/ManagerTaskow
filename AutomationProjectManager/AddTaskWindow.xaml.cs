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
                newTask.Content = "Something witch make You Smile :)";
                newTask.TaskId = 0;
                newTask.TaskType = (TaskTypeEnum)selectTypeCmbox.SelectedItem;
                newTask.UpdateContent();
                newTask.SaveTaskPOST();
            }
            else
            {
                MessageBox.Show("Proszę wybrać typ zadania :)");
            }
            

           
        }
    }
}
