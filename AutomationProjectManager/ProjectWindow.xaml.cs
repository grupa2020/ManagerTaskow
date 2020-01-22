using AutomationProjectManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            projectDataGrid.ItemsSource = taskList;

            /*
            foreach (TaskPoco task in taskList)
            {
                selectBoardBox.Items.Add(task.TaskId);
            }

            if (selectBoardBox.Items.Count >= 1)
            {
                selectBoardBox.SelectedIndex = 0;
            }   */
        }

    }
}
