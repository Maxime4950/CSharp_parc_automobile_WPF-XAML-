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
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Marques.xaml
    /// </summary>
    public partial class Marques : Window
    {
        private ViewModel.VM_Marques LocalMarque;
        public Marques()
        {
            InitializeComponent();
            LocalMarque = new ViewModel.VM_Marques();
            DataContext = LocalMarque;
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgMarque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMarque.SelectedIndex >= 0) LocalMarque.MarqueSelectionnee2UneMarque();
        }
    }
}
