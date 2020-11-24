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
    /// Logique d'interaction pour Desideratas.xaml
    /// </summary>
    public partial class Desideratas : Window
    {
        private ViewModel.VM_Desideratas LocalDesideratas;
        private ViewModel.VM_Personne LocalClients;
        public Desideratas()
        {
            InitializeComponent();
            LocalDesideratas = new ViewModel.VM_Desideratas();
            LocalClients = new ViewModel.VM_Personne();
            ficheDataGridDesideratas.DataContext = LocalDesideratas;
            ficheDataGridClients.DataContext = LocalClients;
            ficheInfosDesiderata.DataContext = LocalDesideratas;
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClients.SelectedIndex >= 0) LocalClients.PersonneSelectionnee2UnePersonne();
        }

        private void dgDesideratas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDesideratas.SelectedIndex >= 0) LocalDesideratas.DesiderataSelectionne2UnDesiderata();
        }

        private void btnConfirmerIdCLient_Click(object sender, RoutedEventArgs e)
        {
            tbIDClients.Text = tbID.Text;
        }
    }
}
