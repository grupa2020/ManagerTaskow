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
        public ProjectWindow(int ProjectId)
        {
            InitializeComponent();
            updateComboBox(ProjectId);
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
            List<BoardPoco> boardList;
            boardList = JsonConvert.DeserializeObject<List<BoardPoco>>(response);

            foreach(BoardPoco board in boardList)
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
            client.serviceUri += "Tasks/" + boardId; //W serwisie musiałaby być 0- to id projektu do którego board należy
            string response = client.getRequest();
            List<TaskPoco> taskList;
            taskList = JsonConvert.DeserializeObject<List<TaskPoco>>(response);


            ///////////////DO TESTÓW


            taskList.Add(new ElectricalProject(12,"bla bla",12));
            taskList.Add(new Maintainence(12, "bla bla", 12));
            taskList.Add(new Mounting(12, "bla bla", 12));
            taskList.Add(new OrderList(12, "bla bla", 12));
            taskList.Add(new ProjectDescription(12, "bla bla", 12));
            taskList.Add(new Workshop(12, "bla bla", 12));
            taskList.Add(new ElectricalProject(12,"bla bla",12));
            taskList.Add(new DriversProject(12, "bla bla", 12));
            ///







            // Tworzenie przycisków tasków i dodawanie ich do DataGrid 


            TaskBoardXY location=new TaskBoardXY();

            
            for(int columnCount=1; columnCount < 9; columnCount++)  //Ilość Kolumn tasków
            {
                tasksGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach (TaskPoco task in taskList)
            {
                TaskButton taskButton = new TaskButton(task);
                taskButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(openTaskWindow));
                
                int dstRow = location.GetDestinationRow(task.Type);
                
                Grid.SetColumn(taskButton, Convert.ToInt32(task.Type));
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
            MessageBox.Show("Otwarcie okna Tasku: " + taskSender.taskId.ToString()+" "+taskSender.Content);
            //   throw new NotImplementedException();
        }

     
    }
}
