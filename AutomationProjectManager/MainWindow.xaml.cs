using AutomationProjectManager.Model;
using AutomationProjectManager.ToolsWindows;
using Newtonsoft.Json;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


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
        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderList listWindow = new OrderList();
            listWindow.Show();
        }

       

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            RestClient client = new RestClient();
            client.method = httpVerb.GET;
            client.serviceUri = "https://localhost:44309/api/Projects/0";
            string response = client.getRequest();
            List<ProjectPoco> projectList;
            projectList = JsonConvert.DeserializeObject<List<ProjectPoco>>(response);
            projectDataGrid.ItemsSource = projectList;

           
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
    }
}
