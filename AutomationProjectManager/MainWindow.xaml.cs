using AutomationProjectManager.Connection;
using AutomationProjectManager.Connection.Responses;
using AutomationProjectManager.Model;
using AutomationProjectManager.ToolsWindows;
using Newtonsoft.Json;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using MaterialDesignColors;
using AutomationProjectManager.DataModels;

namespace AutomationProjectManager
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int OrganizationId;
        

        public MainWindow()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                this.serviceUriTextBox.Text = ConfigurationSettings.AppSettings["ServerPatch"];
            }

            addButton.IsEnabled = false;
            delButton.IsEnabled = false;
            editButton.IsEnabled = false;

            OrganizationId = 1;

            ////TESTY BEZ TOKENA
            Global.LoggedUser = new UsersPoco(1, "Niezalogowany", "","Niezalogowany Użytkownik", 2, 2);
            //Global.LoggedUser.AccessToken = "dsfdsf";
  
        }


        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            RestClient client = new RestClient();
            client.Method = httpVerb.GET;

            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.ServiceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.ServiceUri += "Projects/1";           ///TODO: TUTAJ USTAWIĆ ORG ID JAK JUŻ BĘDZIE GLOBALNE
            string response = client.getRequest();

            if(response!=null)
            {
                var rsponseLst = new ValueResponse<List<ProjectPoco>>(true, string.Empty, null);
                rsponseLst = JsonConvert.DeserializeObject<ValueResponse<List<ProjectPoco>>>(response);
                projectDataGrid.ItemsSource = rsponseLst.Value;
            }
           
        }

        private void projectDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (projectDataGrid.SelectedItem != null)
            {
                ProjectPoco selected = (ProjectPoco)projectDataGrid.SelectedItem;
                ProjectWindow projectWindow = new ProjectWindow(selected.ProjectId, selected.Name);
                projectWindow.Show();
            }

        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Utworzyć i w tym miejscu wywołać metodę która będzie sprawdzac połączenie z serwerem REST
            if (savePropertiesbtn.Content != null)
            {
                UpdateSettings("ServerPatch", serviceUriTextBox.Text);
            }

        }


        /////Zapis ustawień
        public void UpdateSettings(string strKey, string newValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Config.config");

            if (!HasSetting(strKey))
            {
                throw new ArgumentNullException("Key", "<" + strKey + "> does not exist in the configuration. Update failed.");
            }
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    childNode.Attributes["value"].Value = newValue;
            }
            xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Config.config");
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        public bool HasSetting(string strKey)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Config.config");

            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    return true;
            }
            return false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // ProjectPoco newProject = new ProjectPoco("Nowy projekt",DateTime.Now,0,1,"Tenneco",2);

            AddProject projWnd = new AddProject(OrganizationId);
            projWnd.Show();

            //newProject.SaveProjectPOST();
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (projectDataGrid.SelectedItem != null)
            {

                MessageWindow message = new MessageWindow("Czy napewno chcesz usunąć zaznaczony projekt?", true);

                if (message.ShowDialog() == true)
                {
                    ProjectPoco toDelete = (ProjectPoco)projectDataGrid.SelectedItem;
                    SimpleResponse response = JsonConvert.DeserializeObject<SimpleResponse>(toDelete.ProjectDELETE());
                    if (response.Succeeded)
                    {
                        MessageBox.Show("Pomyślnie usunięto projekt");
                    }
                    else
                    {
                        MessageBox.Show("Coś poszło nie tak... \n" + response.Message);
                    }
                    refresh();
                }
            }

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectPoco selected = (ProjectPoco)projectDataGrid.SelectedItem;
            AddProject projWnd = new AddProject(OrganizationId, selected);
            projWnd.Show();
        }



        private void projectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectDataGrid.SelectedItem == null)
            {
                addButton.IsEnabled = false;
                delButton.IsEnabled = false;
                editButton.IsEnabled = false;
            }
            else
            {
                addButton.IsEnabled = true;
                delButton.IsEnabled = true;
                editButton.IsEnabled = true;
            }
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            //Ustawienia Ciemego motywu

            SolidColorBrush backgroundColor = new SolidColorBrush(Color.FromArgb(250, 18, 18, 18));
            SolidColorBrush textAreaColor = new SolidColorBrush(Color.FromArgb(120, 18, 18, 18));
            SolidColorBrush foreColor = new SolidColorBrush(Color.FromRgb(236, 240, 241));
            ResourceDictionary myResourceDictionary = Application.Current.Resources;
            ///MessageBox.Show(myResourceDictionary.Values.ToString());
            myResourceDictionary.Remove("MaterialDesignBackground");
            myResourceDictionary.Add("MaterialDesignBackground", backgroundColor);   //"#121212"       rgb(22, 160, 133)
            myResourceDictionary.Remove("GlobalForeColor");
            myResourceDictionary.Add("GlobalForeColor", foreColor);
            myResourceDictionary.Remove("TextAreasBacground");
            myResourceDictionary.Add("TextAreasBacground", textAreaColor); //TextAreasBacground

        }

        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            //Jasny motyw

            SolidColorBrush backgroundColor = new SolidColorBrush(Color.FromRgb(224, 224, 224));
            //SolidColorBrush whiteSmoke = new SolidColorBrush(Color.FromRgb(225, 229, 229));
            SolidColorBrush textAreaColor = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            ResourceDictionary myResourceDictionary = Application.Current.Resources;
            myResourceDictionary.Remove("MaterialDesignBackground");
            myResourceDictionary.Add("MaterialDesignBackground", backgroundColor);     //  #FFE5E5E5
            myResourceDictionary.Remove("GlobalForeColor");
            myResourceDictionary.Add("GlobalForeColor", Brushes.Black); //Black
            myResourceDictionary.Remove("TextAreasBacground");
            myResourceDictionary.Add("TextAreasBacground", textAreaColor); //FFA6A0A0
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserWindow newWindow = new UserWindow(Global.LoggedUser,false);
            newWindow.Show();
        }

        private void submitLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Authorization sesion = new Authorization(this.usrNameTextBox.Text, this.passTextBox.Password);
            sesion.GetAccess();
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Global.LoggedUser= new UsersPoco(1, "Niezalogowany", "", "Niezalogowany Użytkownik", 2, 2);
            this.usrNameTextBox.Text = "";
            this.passTextBox.Password = "";

            MessageWindow message = new MessageWindow("Wylogowano!");
            message.Show();
        }
        /////////////////////////////////////////////////
    }
}
