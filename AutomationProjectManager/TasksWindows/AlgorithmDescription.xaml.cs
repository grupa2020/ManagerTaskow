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
    /// Logika interakcji dla klasy AlgorithmDescription.xaml
    /// </summary>
    public partial class AlgorithmDescription : Window
    {
        AlgorithmDescriptionTask thisTask;
        public AlgorithmDescription(TaskPoco task)
        {
            InitializeComponent();
            thisTask = new AlgorithmDescriptionTask(task.BoardId,task.Content,task.TaskId);
            fillContent();
        }

        private void fillContent()
        {   // UZUPEŁNIANIE KONTROLEK OKNA
            algDescRichTextBox.AppendText(thisTask.Content);
        }
        string algDescription() //ZWRACA ZAWARTOŚĆ RICHTEXTBOX W STRING
        {
            TextRange textRange = new TextRange(algDescRichTextBox.Document.ContentStart, algDescRichTextBox.Document.ContentEnd);
            return textRange.Text;
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            thisTask.Description = algDescription();
            thisTask.UpdateContent();   //Metoda która pakuje wartości elementów których nie ma klasa rodzica
            thisTask.SaveTaskPUT();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
