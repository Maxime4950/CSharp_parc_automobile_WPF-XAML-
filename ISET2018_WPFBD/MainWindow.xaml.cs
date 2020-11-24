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
using ISET2018_WPFBD.Classes;


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

        FactureVente factV = new FactureVente();

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

                ActualiserDataGridVentes();
                MessageBox.Show ("Vente effectuée N° : " + iID.ToString() + " effectuée");

                //FACTURE

                factV.creerFactureVente(tbIDClientConf, tbNom, tbPre, tbIDVoitureConf, tbNomMarque , tbNomModele,
                tbNomCategorie, tbAnneeFabr, tbNomCarburant, tbNomCouleur, tbKilometrage, tbPrix, dtpDate, tbPaiementID, cbPaiement);

                //Supprimer les frais associé à la voiture que l'on va supprimer
                supprimerFraisAssocieALaVoitSupp();

                //Faire la facture avant de supprimer la voiture et les frais
                new G_StockVoiture(sConnexion).Supprimer(int.Parse(tbIDVoitureConf.Text));

                ActualiserDataGridStock();
                AnnulerInfo();//Une fois tout fini on peut reinitialiser la page
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs ! ");
            }
        }

        private void supprimerFraisAssocieALaVoitSupp()
        {
            FraisDataContext DCFrais = new FraisDataContext();

            var requete = from frais in DCFrais.FraisVoiture
                          where frais.idVoiture == int.Parse(tbIDVoitureConf.Text)
                          select frais.idFrais;

            foreach (var aa in requete)
            {
                new G_Frais(sConnexion).Supprimer(aa);
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

        #region ACTUALISATION DG STOCK (APRES UNE VENTE)

        private void ActualiserDataGridStock()
        {
            LocalStock = new ViewModel.VM_Stock();
            ficheInfoStock.DataContext = LocalStock;
        }
        #endregion

        #region ACTUALISATION DG VENTE (APRES UNE VENTE)
        private void ActualiserDataGridVentes()
        {
            LocalVente = new ViewModel.VM_Vente();
            ficheInfoVentes.DataContext = LocalVente;
        }
        #endregion

        #region Bouton Annuler (reset all tb)

        private void btnAnnulerVente_Click(object sender, RoutedEventArgs e)
        {
            AnnulerInfo();
        }
        #endregion

        #region Remplissage TB en fonction de l'ID
        private void tbIDMarque_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbIDMarque.Text != "")
            {
                MarqueDataContext DCMarque = new MarqueDataContext();
                var requete = from marque in DCMarque.MarqueVoiture
                              where marque.idMarque == int.Parse(tbIDMarque.Text)
                              select marque.nomMarque;

                foreach (var aa in requete)
                {
                    tbNomMarque.Text = aa.ToString();
                }
            }
        }

        private void tbIDModele_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbIDModele.Text != "")
            {
                ModeleDataContext DCModele = new ModeleDataContext();
                var requete = from modele in DCModele.ModeleVoiture
                              where modele.idModele == int.Parse(tbIDModele.Text)
                              select modele.nomModele;

                foreach (var aa in requete)
                {
                    tbNomModele.Text = aa.ToString();
                }
            }
        }

        private void tbIDCategorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbIDCategorie.Text != "")
            {
                CategorieDataContext DCCategorie = new CategorieDataContext();
                var requete = from categorie in DCCategorie.CategorieVoiture
                              where categorie.idCat == int.Parse(tbIDCategorie.Text)
                              select categorie.nomCat;

                foreach (var aa in requete)
                {
                    tbNomCategorie.Text = aa.ToString();
                }
            }
        }


        private void tbIDCarburant_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbIDCarburant.Text != "")
            {
                CarburantDataContext DCCarburant = new CarburantDataContext();
                var requete = from carburant in DCCarburant.CarburantVoiture
                              where carburant.idCarburant == int.Parse(tbIDCarburant.Text)
                              select carburant.nomCarburant;

                foreach (var aa in requete)
                {
                    tbNomCarburant.Text = aa.ToString();
                }
            }
        }

        private void tbIDCouleur_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbIDCouleur.Text != "")
            {
                CouleurDataContext DCCouleur = new CouleurDataContext();
                var requete = from couleur in DCCouleur.CouleurVoiture
                              where couleur.idCouleur == int.Parse(tbIDCouleur.Text)
                              select couleur.nomCouleur;

                foreach (var aa in requete)
                {
                    tbNomCouleur.Text = aa.ToString();
                }
            }
        }
        #endregion
    }
}