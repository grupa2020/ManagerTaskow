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

namespace AutomationProjectManager

{
    /// <summary>
    /// Logika interakcji dla klasy AddKKVariable.xaml
    /// </summary>
    public partial class AddKKSVariable : Window
    {
            private VarDefTool mainWindow = null;


            public AddKKSVariable(VarDefTool mainWin, bool czyNowy = false)
            {
                InitializeComponent();
                mainWindow = mainWin;
                czyNowyProdukt = czyNowy;
                PrzygotujWiazanie();
            }

            private KKS nowyProdukt;

            private bool czyNowyProdukt;

            private void PrzygotujWiazanie()
            {
                KKS produktZListy = mainWindow.lstKKS.SelectedItem as KKS;
                if (!czyNowyProdukt)
                {
                    if (produktZListy != null)
                        gridProdukt.DataContext = produktZListy;
                }
                else
                {
                    nowyProdukt = new KKS("", "", "", "", "", "");
                    gridProdukt.DataContext = nowyProdukt;
                }
            }
            private void btnPotwierdz_Click(object sender, RoutedEventArgs e)
            {
                if (czyNowyProdukt)
                {
                    mainWindow.ListaKKS.Add(nowyProdukt);
                }
                this.Close();
            }


        }
    }
