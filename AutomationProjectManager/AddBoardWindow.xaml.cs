using AutomationProjectManager.Connection.Responses;
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
    /// Logika interakcji dla klasy AddBoardWindow.xaml
    /// </summary>
    /// 
    public partial class AddBoardWindow : Window
    {
        int ProjectId;

        public event EventHandler AddBoardWindow_Closing;


        public AddBoardWindow(int projectId)
        {
            InitializeComponent();
            ProjectId = projectId;
            selectRoleCombobox.SelectedItem = 0;

            
        }

        private void addBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            BoardPoco newBoard = new BoardPoco(this.boardNameTxtBox.Text, ProjectId,selectRoleCombobox.SelectedIndex);
            string info = newBoard.SaveBoardPOST();
            SimpleResponse response = new SimpleResponse();
            response = JsonConvert.DeserializeObject<SimpleResponse>(info);

            
            if (response.Succeeded)
            {
                MessageBox.Show("Pomyślnie dodano nową Tablicę do bazy");
                this.Close();
            }
            else
            {
                MessageBox.Show("Serwer zwrócił błąd: "+ response.Message + "  "+ info);
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.AddBoardWindow_Closing(this, EventArgs.Empty);
        }
    }
}
