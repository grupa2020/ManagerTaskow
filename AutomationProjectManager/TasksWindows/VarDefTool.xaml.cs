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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using AutomationProjectManager.Connection.Responses;

namespace AutomationProjectManager.ToolsWindows
{
    /// <summary>
    /// Logika interakcji dla klasy VarDefTool.xaml
    /// </summary>
    public partial class VarDefTool : Window
    {
        internal ObservableCollection<KKS> ListaKKS = null;

        private TaskPoco mainTask;
        public VarDefTool(TaskPoco task)
        {
            InitializeComponent();
            mainTask = task;
            PrzgotujWiazanie();
        }

        private void PrzgotujWiazanie()
        {
            ListaKKS = new ObservableCollection<KKS>();
            lstKKS.ItemsSource = ListaKKS;

            CollectionView widok = (CollectionView)CollectionViewSource.GetDefaultView(lstKKS.ItemsSource);
            widok.SortDescriptions.Add(new SortDescription("Numer", ListSortDirection.Ascending));
            widok.SortDescriptions.Add(new SortDescription("SymbolUrzadzenia", ListSortDirection.Ascending));

            widok.Filter = FiltrUzytkownika;

            //Wczytanie zapisanej treści
            if(mainTask.Content!=null && mainTask.Content!=string.Empty && mainTask.Content!="")
            {
                ListaKKS = JsonConvert.DeserializeObject<ObservableCollection<KKS>>(mainTask.Content);
            }

            lstKKS.ItemsSource = ListaKKS;
        }
        private bool FiltrUzytkownika(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as KKS).SymbolUrzadzenia.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstKKS.ItemsSource).Refresh();
        }

        private void lstKKS_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            AddKKSVariable okno1 = new AddKKSVariable(this);
            okno1.ShowDialog();
        }


        private void Button_Click_Usn(object sender, RoutedEventArgs e)
        {
            try
            {
                KKS produktZListy = lstKKS.SelectedItem as KKS;
                MessageBoxResult odpowiedz = MessageBox.Show("Czy wykasować zmienną: " + produktZListy.ToString() + "?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if ((odpowiedz == MessageBoxResult.Yes))
                    ListaKKS.Remove(produktZListy);
            }
            catch
            { }
        }



        private void Button_Click_Dod(object sender, RoutedEventArgs e)
        {
            AddKKSVariable okno1 = new AddKKSVariable(this, true);
            okno1.ShowDialog();
        }

        private void lstKKS_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Zapisanie treści
            mainTask.Content = JsonConvert.SerializeObject(ListaKKS);
            SimpleResponse simpleResponse;
            simpleResponse = JsonConvert.DeserializeObject<SimpleResponse>(mainTask.SaveTaskPUT());
            if (simpleResponse.Succeeded)
            {
                MessageWindow message1 = new MessageWindow("Zapisano zmiany pomyślnie!");
                message1.Show();
                
            }
            else
            {
                MessageWindow message1 = new MessageWindow("Coś poszło nie tak..");
                message1.Show();
                
            }

          
            
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow("Czy na pewno chcesz usunąć całe zadanie?", true);

            try
            {
                if (message.ShowDialog() == true)
                {
                    string simpleResponse;
                    simpleResponse = mainTask.DeleteTask();
                  
                    if(simpleResponse!=null)
                    {
                        MessageWindow message1 = new MessageWindow("Usunięto zadanie");
                        message1.Show();
                        this.Close();
                    }
                                      
                }
            }
            catch(Exception exc)
            {
                MessageWindow messageWindow = new MessageWindow(exc.Message);
                messageWindow.Show();
            }
        }
    }
}