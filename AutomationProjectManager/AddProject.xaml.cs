using AutomationProjectManager.Connection.Responses;
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
    /// Logika interakcji dla klasy AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        int OrganizationId;
        ProjectPoco Project;
        bool isNew;
        public AddProject(int organizationId)   //For new project
        {
            InitializeComponent();
            OrganizationId = organizationId;
            datePicker.SelectedDate = DateTime.Now;
            isNew = true;

            DateTime startTime;
            startTime = (DateTime)datePicker.SelectedDate;
            Project = new ProjectPoco(this.nameTextBox.Text, startTime, 1, OrganizationId, AboutClientString(), 0);
        }
        public AddProject(int organizationId, ProjectPoco project)  //For existing projects
        {
            InitializeComponent();
            isNew = false;
            OrganizationId = organizationId;
            datePicker.SelectedDate = DateTime.Now;
            Project = project;

            fillWindow();

           
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(isNew)
            {
                saveNew();
                this.Close();
            }
            else
            {
                updateChanges();
            }
        }

        string AboutClientString()
        {
            TextRange textRange = new TextRange(customerTextBox.Document.ContentStart, customerTextBox.Document.ContentEnd);
            return textRange.Text;
        }

        private void fillWindow()
        {
            nameTextBox.Text = Project.Name;
            datePicker.SelectedDate = Project.StartDate;
            customerTextBox.AppendText(Project.CustomerName);
        }

        private void saveNew()
        {
            if (datePicker.SelectedDate != null)
            {
                string info = string.Empty;
                Project.StartDate = (DateTime)datePicker.SelectedDate;
                Project.Name = nameTextBox.Text;
                Project.CustomerName = AboutClientString();

                info = Project.SaveProjectPOST();

                SimpleResponse responseMsg = JsonConvert.DeserializeObject<SimpleResponse>(info);

                if (responseMsg.Succeeded)
                {
                    MessageWindow message = new MessageWindow("Pomyślnie dodano nowy projekt");
                    message.Show();

                    isNew = false;

                }
                else
                {
                    MessageWindow message = new MessageWindow("Coś poszło nie tak :( \n Odpowiedź serwera: \n" + responseMsg.Message);
                    message.Show();
                }

            }
        }

        private void updateChanges()
        {
            if (datePicker.SelectedDate != null)
            {
                string info = string.Empty;
                Project.StartDate = (DateTime)datePicker.SelectedDate;
                Project.Name = nameTextBox.Text;
                Project.CustomerName = AboutClientString();

                info = Project.SaveProjectPUT();

                SimpleResponse responseMsg = JsonConvert.DeserializeObject<SimpleResponse>(info);

                if (responseMsg.Succeeded)
                {
                    MessageWindow message = new MessageWindow("Pomyślnie zapisano zmiany");
                    message.Show();

                    isNew = false;

                }
                else
                {
                    MessageWindow message = new MessageWindow("Coś poszło nie tak :( \n Odpowiedź serwera: \n" + responseMsg.Message);
                    message.Show();
                }

            }
        }

     

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaksimizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
