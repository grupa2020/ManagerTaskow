using AutomationProjectManager.Connection.Responses;
using AutomationProjectManager.DataModels.TasksChildrens;
using AutomationProjectManager.Factories;
using AutomationProjectManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Logika interakcji dla klasy ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        int ProjectId;
        public ProjectWindow(int projectId)
        {
            InitializeComponent();
            updateComboBox(projectId);
            ProjectId = projectId;
            loadTasks();                      
        }


        public void updateComboBox(int ProjectId)
        {
            RestClient client = new RestClient();
            client.method = httpVerb.GET;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.serviceUri += "Boards/"+ProjectId.ToString(); //W serwisie musiałaby być 0- to id projektu do którego board należy
            string response = client.getRequest();

            var rsponseLst = new ValueResponse<List<BoardPoco>>(true, string.Empty, null);
            rsponseLst = JsonConvert.DeserializeObject<ValueResponse<List<BoardPoco>>>(response);

            foreach(BoardPoco board in rsponseLst.Value)
            {
                selectBoardBox.Items.Add(board.Name+"/"+board.BoardId.ToString());
            }

            if(selectBoardBox.Items.Count>=1)
            {
                selectBoardBox.SelectedIndex = 0;
            }
        }

        public void loadTasks()
        {
            string[] list = selectBoardBox.SelectedItem.ToString().Split('/');
            string boardId = "-1";
            if(list.Length>=2)
            {
                boardId =list[1];
            }

            RestClient client = new RestClient();
            client.method = httpVerb.GET;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.serviceUri += "Tasks/" + boardId; 
            string response = client.getRequest();

            var taskList = new ValueResponse<List<TaskPoco>>(true, string.Empty, null);
            taskList = JsonConvert.DeserializeObject<ValueResponse<List<TaskPoco>>>(response);


            ///////////////DO TESTÓW
            /*
            taskList.Value.Add(new AlgorithmDescriptionTask(12, "bla bla", 12));
            taskList.Value.Add(new ElectricalProject(12,"bla bla",12));
            taskList.Value.Add(new Maintainence(12, "bla bla", 12));
            taskList.Value.Add(new Mounting(12, "bla bla", 12));
            taskList.Value.Add(new OrderList(12, "bla bla", 12));
            taskList.Value.Add(new ProjectDescription(12, "bla bla", 12));
            taskList.Value.Add(new Workshop(12, "bla bla", 12));
            taskList.Value.Add(new ElectricalProject(12,"bla bla",12));
            taskList.Value.Add(new DriversProject(12, "bla bla", 12));
            taskList.Value.Add(new VarDefTool(12, "bla bla", 12));
            */
            ///

            // Tworzenie przycisków tasków i dodawanie ich do DataGrid 


            TaskBoardXY location=new TaskBoardXY();

            
            for(int columnCount=1; columnCount < 7; columnCount++)  //Ilość Kolumn tasków
            {
                tasksGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach (TaskPoco task in taskList.Value)
            {
                TaskButton taskButton = new TaskButton(task);
                taskButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(openTaskWindow));
                
                int dstRow = location.GetDestinationRow(task.TaskType);
                
                Grid.SetColumn(taskButton, location.GetColumn(task.TaskType));
                Grid.SetRow(taskButton, dstRow-1);
 
                tasksGrid.Children.Add(taskButton);
            }

            int rowsCount = location.GetMax();
            for(int i=0;i<rowsCount;i++)
            {
                tasksGrid.RowDefinitions.Add(new RowDefinition());
            }
        }


        private void openTaskWindow(object sender, RoutedEventArgs e)
        {
            TaskButton taskSender= sender as TaskButton;
            MessageBox.Show("Otwarcie okna Tasku: " + taskSender.taskId.ToString()+" "+taskSender.GetContent());
            //   throw new NotImplementedException();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow newTaskWindow = new AddTaskWindow(1);//Tu zmienić na wybrany board w comboboxie
            newTaskWindow.Show();
        }

        private void AddBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBoardWindow newBoardWnd = new AddBoardWindow(ProjectId);
            newBoardWnd.Show();
        }

        private void selectBoardBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tasksGrid.Children.Clear();
            tasksGrid.RowDefinitions.Clear();
            tasksGrid.ColumnDefinitions.Clear();
            loadTasks();

        }
    }
}
