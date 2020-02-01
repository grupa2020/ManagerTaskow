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
    /// Logika interakcji dla klasy UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        UsersPoco CurrentUser;
        bool IsNew;
        public UserWindow(UsersPoco currentUser, bool isNew)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            IsNew = isNew;

            if(CurrentUser!=null)
            {
                this.UserNameTxtBox.Text = CurrentUser.Name;
                this.UserLoginTxtBox.Text = CurrentUser.Login;
                this.UserPassTxtBox.Password = CurrentUser.Password;
                
                if(this.CurrentUser.Role!=0)
                {
                    this.AddUser.Visibility = Visibility.Hidden;
                    this.roleComboBox.IsEnabled = false;
                }

                


                this.roleComboBox.SelectedIndex = CurrentUser.Role;
            }

            if(IsNew)
            {
                this.saveUser.Content = "Dodaj użytkownika";
                this.roleComboBox.IsEnabled = true;
            }
            
            
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.Login = this.UserLoginTxtBox.Text;
            CurrentUser.Name = this.UserNameTxtBox.Text;
            CurrentUser.Password = this.UserPassTxtBox.Password;
            if(IsNew)
            {
                CurrentUser.Role = this.roleComboBox.SelectedIndex;
                SimpleResponse response = JsonConvert.DeserializeObject<SimpleResponse>(CurrentUser.AddUserPOST());
                if(response.Succeeded)
                {
                    MessageWindow message = new MessageWindow("Pomyślnie dodano użytkownika.");
                    message.Show();
                    this.Close();
                }
                else
                {
                    MessageWindow message = new MessageWindow("Coś poszło nie tak .... :("+ response.Message);
                    message.Show();
                }
              
            }
            else
            {
                SimpleResponse response = JsonConvert.DeserializeObject<SimpleResponse>(CurrentUser.SaveUserPUT());
                
                if(response.Succeeded)
                {
                    MessageWindow message = new MessageWindow("Pomyślnie zapisano zmiany :)");
                    message.Show();
                }
                else
                {
                    MessageWindow message = new MessageWindow("Coś poszło nie tak .... :(" + response.Message);
                    message.Show();
                }
              
            }
          
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            UsersPoco newUser = new UsersPoco(0, "new user login", "", "new user name", 1, 1);
            UserWindow addUserWindow = new UserWindow(newUser,true);
            addUserWindow.Show();
        }
    }
}
