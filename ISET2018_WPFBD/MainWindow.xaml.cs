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
        public MainWindow()
        {
            InitializeComponent();
            LocalPersonne = new ViewModel.VM_Personne();
            LocalStock = new ViewModel.VM_Stock();

            ficheInfoClient.DataContext = LocalPersonne;
            ficheInfoStock.DataContext = LocalStock;
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


        private void dgClientsTabBord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientsTabBord.SelectedIndex >= 0)
            {
                LocalPersonne.PersonneSelectionnee2UnePersonne();
            }
        }

        private void dgStockTabBord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStockTabBord.SelectedIndex >= 0)
            {
                LocalStock.StockSelectionnee2UnStock();
            }
        }
    }
}