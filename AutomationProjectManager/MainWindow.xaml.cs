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

namespace AutomationProjectManager
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                this.serviceUriTextBox.Text = ConfigurationSettings.AppSettings["ServerPatch"];
            }
        }
       

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            RestClient client = new RestClient();
            client.method = httpVerb.GET;
           
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["ServerPatch"]))
            {
                client.serviceUri = ConfigurationSettings.AppSettings["ServerPatch"];
            }
            client.serviceUri +="Projects/1";           ///TODO: TUTAJ USTAWIĆ ORG ID JAK JUŻ BĘDZIE GLOBALNE
            string response = client.getRequest();

            var rsponseLst =new ValueResponse<List<ProjectPoco>>(true,string.Empty,null);
            rsponseLst=JsonConvert.DeserializeObject<ValueResponse<List<ProjectPoco>>>(response);
            projectDataGrid.ItemsSource = rsponseLst.Value;

            


        }

        private void projectDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(projectDataGrid.SelectedItem!=null)
            {
                ProjectPoco selected = (ProjectPoco)projectDataGrid.SelectedItem;
                ProjectWindow projectWindow = new ProjectWindow(selected.ProjectId);
                projectWindow.Show();
            }
          
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Utworzyć i w tym miejscu wywołać metodę która będzie sprawdzac połączenie z serwerem REST
            if(savePropertiesbtn.Content!=null)
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
            ProjectPoco newProject = new ProjectPoco("Nowy projekt",DateTime.Now,0,1,"Tenneco");

            AddProject projWnd = new AddProject();
            projWnd.Show();
            
            //newProject.SaveProjectPOST();
        }

        /////////////////////////////////////////////////
    }
}
