using AutomationProjectManager.Connection.Responses;
using AutomationProjectManager.DataModels.TasksChildrens;
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

namespace AutomationProjectManager.ToolsWindows
{
    /// <summary>
    /// Logika interakcji dla klasy ProjectDescription.xaml
    /// </summary>
    public partial class ProjectDescriptionWindow : Window
    {
        ProjectDescription thisTask;

        public ProjectDescriptionWindow(TaskPoco task)
        {
            InitializeComponent();
            thisTask = new ProjectDescription(task.BoardId, task.Content, task.TaskId);

            fillContent();
        }
        private void fillContent()
        {
            algDescRichTextBox.Document.Blocks.Clear();
            algDescRichTextBox.Document.Blocks.Add(new Paragraph(new Run(thisTask.Description)));
        }
        string projDescription()
        {
            TextRange textRange = new TextRange(algDescRichTextBox.Document.ContentStart, algDescRichTextBox.Document.ContentEnd);
            return textRange.Text;
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            thisTask.Description = projDescription();
            thisTask.UpdateContent();

            SimpleResponse response = JsonConvert.DeserializeObject<SimpleResponse>(thisTask.SaveTaskPUT());

            if (response != null)
            {
                if (response.Succeeded)
                {
                    MessageWindow message = new MessageWindow("Pomyślnie zapisano zmiany");
                    message.Show();
                }
                else
                {
                    MessageWindow message = new MessageWindow("Coś poszło nie tak...\n" + response.Message);
                    message.Show();
                }
            }
            else
            {
                MessageWindow message = new MessageWindow("Coś poszło nie tak... \n Sprawdź połączenie z serwerem REST");
                message.Show();
            }




        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                MessageWindow question = new MessageWindow("Czy na pewno chcesz usunąć całe zadanie?", true);

                if (question.ShowDialog() == true)
                {
                    string responseInfo = thisTask.DeleteTask();

                    if (responseInfo != string.Empty && responseInfo != null)
                    {

                        MessageWindow message = new MessageWindow(responseInfo);
                        message.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageWindow message = new MessageWindow("Coś poszło nie tak... \n Sprawdź połączenie z serwerem REST");
                        message.Show();
                    }


                }

            }

            catch (Exception exc)
            {
                MessageWindow message = new MessageWindow(exc.Message);
                message.Show();

            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
    }
}