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
    /// Logique d'interaction pour AchatVente.xaml
    /// </summary>
    public partial class Vente : Window
    {
        private ViewModel.VM_Vente LocalVente;
        public Vente()
        {
            InitializeComponent();
            LocalVente = new ViewModel.VM_Vente();
            DataContext = LocalVente;
        }

        private void dgVentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVentes.SelectedIndex >= 0) LocalVente.VenteSelectionnee2UneVente();
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
