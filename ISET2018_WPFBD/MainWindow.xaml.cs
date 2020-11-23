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
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;
using ISET2018_WPFBD.DataAccess.DataObject;

namespace ISET2018_WPFBD

{
     /// <summary>
     /// Logique d'interaction pour MainWindow.xaml
     /// </summary>
      public partial class MainWindow : Window
      {
        #region Initialisation des variables
        private ViewModel.VM_Personne LocalPersonne;
        private ViewModel.VM_Stock LocalStock;
        private ViewModel.VM_Vente LocalVente;
        private ViewModel.VM_Vente LocalVenteActualiser;

        private string sConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        #endregion

        #region Constructeur MainWindow()
        public MainWindow()
        {
            InitializeComponent();
            LocalPersonne = new ViewModel.VM_Personne();
            LocalStock = new ViewModel.VM_Stock();
            LocalVente = new ViewModel.VM_Vente();

            ficheInfoVentes.DataContext = LocalVente;
            ficheInfoClient.DataContext = LocalPersonne;
            ficheInfoStock.DataContext = LocalStock;

            remplirCbPaiement();
        }
        #endregion

        #region BOUTON DU MENU GAUCHE => EVENEMENTS CLICK
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

        private void btnAchats_Click(object sender, RoutedEventArgs e)
        {
            View.Achats f = new View.Achats();
            f.ShowDialog();
        }

        private void btnAjoutAchats_Click(object sender, RoutedEventArgs e)
        {
            View.AjoutAchat f = new View.AjoutAchat();
            f.ShowDialog();
        }
        #endregion

        #region 3 DGV => EVENEMENTS SELECTIONCHANGED

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

        #endregion

        #region CONFIRMATION DE LA VENTE

        private void btnConfirmerVente_Click(object sender, RoutedEventArgs e)
        {
            if (tbIDClientConf.Text != "" && tbIDVoitureConf.Text != "" && tbPrix.Text != "" && dtpDate.Text != "" && tbPaiementID.Text != "")
            {
                int iID = new G_AchatVente(sConnexion).Ajouter(int.Parse(tbIDVoitureConf.Text), int.Parse(tbIDClientConf.Text), int.Parse(tbPrix.Text)
                 , DateTime.Parse(dtpDate.Text), int.Parse(tbPaiementID.Text), "vente");
                AnnulerInfo(); //Pour vider les textbox
                ActualiserDataGridVentes();
                MessageBox.Show ("Vente effectuée N° : " + iID.ToString() + " effectuée");
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs ! ");
            }
        }

        #endregion

        #region VIDER TOUTES LES TEXTBOX

        //Pour vider les textBoxs
        private void AnnulerInfo()
        {
            tbIDVoitureConf.Text = "";
            tbIDClientConf.Text = "";
            dtpDate.Text = "";
            tbPrix.Text = "";
        }
        #endregion

        #region Combobox et texbox Paiement
        private void remplirCbPaiement()
        {
            PaiementDataContext DCPaiement = new PaiementDataContext();
            var requete = from paiement in DCPaiement.PaiementVoiture
                          select paiement.nomPaiement;

            foreach (var aa in requete)
            {
                cbPaiement.Items.Add(aa.ToString());
            }
        }

        private void cbPaiement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PaiementDataContext DCPaiement = new PaiementDataContext();
            var requete = from paiement in DCPaiement.PaiementVoiture
                          where paiement.nomPaiement == cbPaiement.SelectedItem.ToString()
                          select paiement.idPaiement;

            foreach (var aa in requete)
            {
                tbPaiementID.Text = aa.ToString();
            }

        }
        #endregion

        #region ACTUALISATION DG VENTE (APRES UNE VENTE)
        private void ActualiserDataGridVentes()
        {
            LocalVenteActualiser = new ViewModel.VM_Vente();
            ficheInfoVentes.DataContext = LocalVenteActualiser;
        }
        #endregion

        #region Bouton Annuler (reset all tb)

        private void btnAnnulerVente_Click(object sender, RoutedEventArgs e)
        {
            AnnulerInfo();
        }
        #endregion
      }
}