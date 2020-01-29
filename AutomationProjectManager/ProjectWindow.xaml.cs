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
        int BoardId;
        List<BoardPoco> BoardList;

        public ProjectWindow(int projectId, string projectName)
        {
            InitializeComponent();
            ProjectId = projectId;
            projectNameLbl.Content = projectName;
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
            client.Method = httpVerb.GET;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.ServiceUri += "Boards/" + ProjectId.ToString(); //W serwisie musiałaby być 0- to id projektu do którego board należy
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
                client.Method = httpVerb.GET;
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Tasks/" + BoardId;
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
            TaskWindowMaker maker = new TaskWindowMaker(taskSender.Task);
            Window newTaskWindow = maker.GetWindow();
            newTaskWindow.Closed += new EventHandler(AddTaskWnd_Closing);
            newTaskWindow.Show();
            //   throw new NotImplementedException();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow newTaskWindow = new AddTaskWindow(BoardId);//Tu zmienić na wybrany board w comboboxie
            newTaskWindow.AddTaskWnd_Closing += new EventHandler(AddTaskWnd_Closing);
            newTaskWindow.Show();
        }

        private void AddTaskWnd_Closing(object sender, EventArgs e)
        {
            loadTasks();
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

            MessageWindow message = new MessageWindow("Czy napewno chcesz usunąć widoczną tablicę?", true);

            if (message.ShowDialog() == true)
            {

                RestClient client = new RestClient();
                client.Method = httpVerb.DELETE;
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
                {
                    client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
                }
                client.ServiceUri += "Boards/" + BoardId.ToString();

                int index = BoardList.FindIndex(x => x.BoardId == BoardId);



                SimpleResponse response = JsonConvert.DeserializeObject<SimpleResponse>(client.DeleteMethod(BoardList[index]));
                if (response.Succeeded)
                {
                    MessageWindow message2 = new MessageWindow("Pomyślnie usunięto tablicę");
                    message2.Show();
                }
                else
                {
                    MessageWindow message2 = new MessageWindow("Wystąpił problem: \n serwer odpowiedział: \n" + response.Message);
                    message2.Show();

                }

                load();
            }

        }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tasksGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MaksimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.SizeToContent = SizeToContent.Width;
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.SizeToContent = SizeToContent.Manual;
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

        private void DragWindow_Click(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}
