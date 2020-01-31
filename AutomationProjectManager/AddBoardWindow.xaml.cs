using AutomationProjectManager.Connection.Responses;
using AutomationProjectManager.DataModels;
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
            selectRoleCombobox.SelectedIndex = Global.LoggedUser.Role;
            setPermisions();

            
        }

        private void setPermisions()
        {
            switch (Global.LoggedUser.Role)
            {
                case 0:
                    {
                        //Nie robi nic użytkownik ma prawo do wszystkiego
                    }
                    break;

                case 1:
                    {
                        item0.IsEnabled = false;
                    }
                    break;

                case 2:
                    {
                        item0.IsEnabled = false;
                        item1.IsEnabled = false;
                    }
                    break;

                case 3:
                    {
                        item0.IsEnabled = false;
                        item1.IsEnabled = false;
                        item2.IsEnabled = false;
                    }
                    break;

                default:
                    {

                    }
                    break;
            }


        }

        private void addBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            BoardPoco newBoard = new BoardPoco(this.boardNameTxtBox.Text, ProjectId,selectRoleCombobox.SelectedIndex);
            string info = newBoard.SaveBoardPOST();
            SimpleResponse response = new SimpleResponse();
            response = JsonConvert.DeserializeObject<SimpleResponse>(info);

            
            if (response.Succeeded)
            {
                MessageWindow message2 = new MessageWindow("Pomyślnie dodano nową Tablicę do bazy");
                message2.Show();
               
                this.Close();
            }
            else
            {
                MessageWindow message2 = new MessageWindow("Serwer zwrócił błąd: " + response.Message + "  " + info);
                message2.Show();
               
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.AddBoardWindow_Closing(this, EventArgs.Empty);
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
    }
}
