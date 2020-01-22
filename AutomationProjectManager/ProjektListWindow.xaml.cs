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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutomationProjectManager
{
    /// <summary>
    /// Interaction logic for ProjektListWindow.xaml
    /// </summary>
    public partial class ProjektListWindow : UserControl
    {
        public ProjektListWindow()
        {
            InitializeComponent();
            InitProjects();
        }

        void InitProjects()
        {
            for(int i =0; i< 3;i++)
            {
                ProjectLabel pLable = new ProjectLabel();
                ProjectsList.Children.Add(pLable);
            }
        }

    }
}
