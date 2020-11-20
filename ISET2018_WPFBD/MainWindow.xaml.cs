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

namespace ISET2018_WPFBD

{
     /// <summary>
     /// Logique d'interaction pour MainWindow.xaml
     /// </summary>
      public partial class MainWindow : Window
      {
        private ViewModel.VM_Personne LocalPersonne;
        private ViewModel.VM_Stock LocalStock;
        private ViewModel.VM_Vente LocalVente;
        private ViewModel.VM_Paiement LocalPaiement;
        public MainWindow()
        {
            InitializeComponent();
            LocalPersonne = new ViewModel.VM_Personne();
            LocalStock = new ViewModel.VM_Stock();
            LocalVente = new ViewModel.VM_Vente();
            LocalPaiement = new ViewModel.VM_Paiement();

            ficheInfoVentes.DataContext = LocalVente;
            ficheInfoClient.DataContext = LocalPersonne;
            ficheInfoStock.DataContext = LocalStock;

            

            //Ajout des modes de paiements
            for(int i = 0; i < LocalPaiement.BcpPaiement.Count(); i++)
            {
                cbPaiement.Items.Add(LocalPaiement.BcpPaiement[i].nomPaiement);
            }
        }
        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        { 
            Close(); 
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            View.EcranPersonne f = new View.EcranPersonne();
            f.ShowDialog();
        }

        private void btnMarques_Click(object sender, RoutedEventArgs e)
        {
            View.Marques f = new View.Marques();
            f.ShowDialog();
        }

        private void btnModeles_Click(object sender, RoutedEventArgs e)
        {
            View.Modeles f = new View.Modeles();
            f.ShowDialog();
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            View.Stock f = new View.Stock();
            f.ShowDialog();
        }

        private void btnVentes_Click(object sender, RoutedEventArgs e)
        {
            View.Vente f = new View.Vente();
            f.ShowDialog();
        }


        private void dgClientsTabBord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientsTabBord.SelectedIndex >= 0)
            {
                LocalPersonne.PersonneSelectionnee2UnePersonne();
                tbIDClientConf.Text =  tbID.Text;
            }
        }

        private void dgStockTabBord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStockTabBord.SelectedIndex >= 0)
            {
                LocalStock.StockSelectionnee2UnStock();
                tbIDVoitureConf.Text = tbIDVoiture.Text;
            }
        }


        private void dgVentesTabBord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVentesTabBord.SelectedIndex >= 0)
            {
                LocalVente.VenteSelectionnee2UneVente();
            }
        }
    }
}