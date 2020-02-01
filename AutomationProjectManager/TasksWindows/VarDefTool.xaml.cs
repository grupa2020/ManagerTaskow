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

namespace AutomationProjectManager.ToolsWindows
{
    /// <summary>
    /// Logika interakcji dla klasy VarDefTool.xaml
    /// </summary>
    public partial class VarDefTool : Window
    {
        internal ObservableCollection<KKS> ListaKKS = null;

        public VarDefTool(TaskPoco task)
        {
            InitializeComponent();
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
    }
}