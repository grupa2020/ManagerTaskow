using AutomationProjectManager.DataModels.TasksChildrens;
using AutomationProjectManager.Model;
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
    public partial class ProjectDescription : Window
    {
        AlgorithmDescriptionTask thisTask;

        public ProjectDescription(TaskPoco task)
        {
            InitializeComponent();
            thisTask = new AlgorithmDescriptionTask(task.BoardId, task.Content, task.TaskId);
            fillContent();
        }
        private void fillContent()
        {
            algDescRichTextBox.AppendText(thisTask.Content);
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
            thisTask.SaveTaskPUT();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show(thisTask.DeleteTask());
            this.Close();
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