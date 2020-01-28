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

namespace AutomationProjectManager
{
    /// <summary>
    /// Logika interakcji dla klasy MessageWindow.xaml
    /// </summary>
   
    public partial class MessageWindow : Window
    {


        bool YesNo;

        public MessageWindow(string message)
        {
            InitializeComponent();
            Message.Text = message;
            this.noBtn.Visibility = Visibility.Hidden;
            this.yesBtn.Content = "OK";
            YesNo = false;
        }
        public MessageWindow(string message,bool yesNo) //jeśli yesNo == true to będzie zwracać odpowiedź użytkownika
        {
            InitializeComponent();
            Message.Text = message;
            YesNo = true;
            
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            if(YesNo)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                this.Close();
            }
           
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
