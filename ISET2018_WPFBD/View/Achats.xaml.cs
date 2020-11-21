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
    /// Logique d'interaction pour Achats.xaml
    /// </summary>
    public partial class Achats : Window
    {
        private ViewModel.VM_Achat LocalAchat;
        public Achats()
        {
            InitializeComponent();
            LocalAchat = new ViewModel.VM_Achat();
            DataContext = LocalAchat;
        }

        private void dgAchats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAchats.SelectedIndex >= 0) LocalAchat.AchatSelectionnee2UnAchat();
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
