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
 /// Logique d'interaction pour Personne.xaml
 /// </summary>
 public partial class EcranPersonne : Window
    {
      private ViewModel.VM_Personne LocalPersonne;
      public EcranPersonne()
      {
            InitializeComponent();
            LocalPersonne = new ViewModel.VM_Personne();
            DataContext = LocalPersonne;
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClients.SelectedIndex >= 0) LocalPersonne.PersonneSelectionnee2UnePersonne();
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
