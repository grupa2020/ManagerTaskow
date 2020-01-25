using AutomationProjectManager.Model;
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
    /// Logika interakcji dla klasy AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        int OrganizationId;
        public AddProject(int organizationId)
        {
            InitializeComponent();
            OrganizationId = organizationId;
            datePicker.SelectedDate = DateTime.Now;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime startTime;           
            if(datePicker.SelectedDate!=null)
            {
                startTime = (DateTime)datePicker.SelectedDate;
                ProjectPoco newProject = new ProjectPoco(this.nameTextBox.Text, startTime, 1, OrganizationId, "name", 0);
                MessageBox.Show(newProject.SaveProjectPOST());
            }
            
        }
    }
}
