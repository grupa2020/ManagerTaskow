using AutomationProjectManager.Model;
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
        }

        public void updateComboBox(int ProjectId)
        {
            RestClient client = new RestClient();
            client.method = httpVerb.GET;
            client.serviceUri = "https://localhost:44309/api/Boards/"+ProjectId.ToString(); //W serwisie musiałaby być 0- to id projektu do którego board należy
            string response = client.getRequest();
            List<BoardPoco> boardList;
            boardList = JsonConvert.DeserializeObject<List<BoardPoco>>(response);

            foreach(BoardPoco board in boardList)
            {
                selectBoardBox.Items.Add(board.Name);
            }
           
        }
    }
}
