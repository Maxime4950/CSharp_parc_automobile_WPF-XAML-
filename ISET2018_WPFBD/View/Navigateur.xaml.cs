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

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Navigateur.xaml
    /// </summary>
    public partial class Navigateur : Window
    {
        public Navigateur()
        {
            InitializeComponent();
            wb.Navigate("C:/Users/Maesm/Documents/Complement_P/ISET2018_WPFBD_MVVM_concept/ISET2018_WPFBD/bin/Debug/Stock.html");
        }
    }
}
