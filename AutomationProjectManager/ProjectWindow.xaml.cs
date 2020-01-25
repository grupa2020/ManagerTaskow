﻿using AutomationProjectManager.Connection.Responses;
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
        int BoardId;
        List<BoardPoco> BoardList;
        public ProjectWindow(int projectId)
        {
            InitializeComponent();
            ProjectId = projectId;
            load();
        }

        public void load()
        {
            tasksGrid.Children.Clear();
            tasksGrid.RowDefinitions.Clear();
            tasksGrid.ColumnDefinitions.Clear();
            selectBoardBox.Items.Clear();

            BoardList = new List<BoardPoco>();

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
            client.serviceUri += "Boards/" + ProjectId.ToString(); //W serwisie musiałaby być 0- to id projektu do którego board należy
            string response = client.getRequest();

            var rsponseLst = new ValueResponse<List<BoardPoco>>(true, string.Empty, null);
            rsponseLst = JsonConvert.DeserializeObject<ValueResponse<List<BoardPoco>>>(response);

            foreach (BoardPoco board in rsponseLst.Value)
            {
                selectBoardBox.Items.Add(board.Name + "/" + board.BoardId.ToString());
                BoardList.Add(board);
            }

            if (selectBoardBox.Items.Count >= 1)
            {
                selectBoardBox.SelectedIndex = 0;
            }
        }

        public void loadTasks()
        {
            tasksGrid.Children.Clear();
            tasksGrid.RowDefinitions.Clear();
            tasksGrid.ColumnDefinitions.Clear();

            if (selectBoardBox.SelectedItem != null)
            {


                string[] list = selectBoardBox.SelectedItem.ToString().Split('/');

                if (list.Length >= 2)
                {
                    BoardId = Convert.ToInt32(list[1]);
                }

                RestClient client = new RestClient();
                client.method = httpVerb.GET;
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.serviceUri += "Tasks/" + BoardId;
                string response = client.getRequest();

                var taskList = new ValueResponse<List<TaskPoco>>(true, string.Empty, null);
                taskList = JsonConvert.DeserializeObject<ValueResponse<List<TaskPoco>>>(response);

                TaskBoardXY location = new TaskBoardXY();

                for (int columnCount = 0; columnCount < 6; columnCount++)  //Ilość Kolumn tasków
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    //colDef.Width = GridLength.Auto;
                    
                    tasksGrid.ColumnDefinitions.Add(colDef);
                }

                foreach (TaskPoco task in taskList.Value)
                {
                    TaskButton taskButton = new TaskButton(task);
                    taskButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(openTaskWindow));

                    int dstRow = location.GetDestinationRow(task.TaskType);

                    Grid.SetColumn(taskButton, location.GetColumn(task.TaskType));
                    Grid.SetRow(taskButton, dstRow - 1);

                    tasksGrid.Children.Add(taskButton);
                }

                int rowsCount = location.GetMax();
                for (int i = 0; i < rowsCount; i++)
                {
                    RowDefinition rowDef = new RowDefinition();
                    rowDef.Height = GridLength.Auto;
                    tasksGrid.RowDefinitions.Add(rowDef);
                }
            }

        }


        private void openTaskWindow(object sender, RoutedEventArgs e)
        {
            TaskButton taskSender = sender as TaskButton;
            MessageBox.Show("Otwarcie okna Tasku: " + taskSender.taskId.ToString() + " " + taskSender.GetContent());
            //   throw new NotImplementedException();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow newTaskWindow = new AddTaskWindow(BoardId);//Tu zmienić na wybrany board w comboboxie
            newTaskWindow.Show();
        }

        private void AddBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBoardWindow newBoardWnd = new AddBoardWindow(ProjectId);
            newBoardWnd.AddBoardWindow_Closing += new EventHandler(AddBoardWnd_Closing);
            newBoardWnd.Show();
        }
        void AddBoardWnd_Closing(object sender, EventArgs e)
        {
            //Łapanie zdarzenia wyłączenia okna dodawania AddBoardWindow
            load();
        }

        private void selectBoardBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tasksGrid.Children.Clear();
            tasksGrid.RowDefinitions.Clear();
            tasksGrid.ColumnDefinitions.Clear();
            loadTasks();

        }

        private void DeleteBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            RestClient client = new RestClient();
            client.method = httpVerb.DELETE;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.serviceUri += "Boards/" + BoardId.ToString();

            int index = BoardList.FindIndex(x => x.BoardId == BoardId);

            MessageBox.Show(client.DeleteMethod(BoardList[index]));

            load();

        }

     
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tasksGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
