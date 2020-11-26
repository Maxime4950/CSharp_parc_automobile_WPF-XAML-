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
using ISET2018_WPFBD.Model;
using ISET2018_WPFBD.DataAccess.DataObject;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Desideratas.xaml
    /// </summary>
    public partial class Desideratas : Window
    {
        private ViewModel.VM_Desideratas LocalDesideratas;
        private ViewModel.VM_Personne LocalClients;
        private string chConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        public Desideratas()
        {
            InitializeComponent();
            LocalDesideratas = new ViewModel.VM_Desideratas();
            LocalClients = new ViewModel.VM_Personne();
            ficheDataGridDesideratas.DataContext = LocalDesideratas;
            ficheDataGridClients.DataContext = LocalClients;
            ficheInfosDesiderata.DataContext = LocalDesideratas;
            //Activation des boutons au lancement de la page
            gestionEnabledBoutonMenu(true);
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
            tbIDClientsDesiderata.Text = tbID.Text;
            remplirTBCBDesiderata();
            ficheInfosDesiderata.IsEnabled = true;
            ficheDataGridClients.IsEnabled = false;
        }

        private void bConsulter_Click(object sender, RoutedEventArgs e)
        {
            if (dgDesideratas.SelectedCells.Count > 0)
            {

                tbIDDesiderata.Text = LocalDesideratas.DesiderataSelectionne.idDesiterata.ToString();
                C_Desideratas pTmp = new G_Desideratas(chConnexion).Lire_ID(int.Parse(tbIDDesiderata.Text));

                //Client
                tbIDClientsDesiderata.Text = pTmp.idClient.ToString();

                //Marque
                tbIDMarqueDesiderata.Text = pTmp.idMarque.ToString();
                C_Marque MarqueTmp = new G_Marque(chConnexion).Lire_ID(int.Parse(tbIDMarqueDesiderata.Text));
                cbNomMarqueDesiderata.Text = MarqueTmp.nomMarque;

                //Modele
                tbIDModeleDesiderata.Text = pTmp.idModele.ToString();
                C_Modele ModeleTmp = new G_Modele(chConnexion).Lire_ID(int.Parse(tbIDModeleDesiderata.Text));
                cbNomModeleDesiderata.Text = ModeleTmp.nomModele;

                //Catégorie
                tbIDCategorieDesiderata.Text = pTmp.idCat.ToString();
                //c_cate CatTmp = new G_CategorieVoiture(sConnex).Lire_ID(int.Parse(tbIdCategorie.Text));
                //cbNomCategorie.Text = CatTmp.nomCat;

                //Année de fabrication min
                tbAnneeFabrDesiderata.Text = pTmp.anneeMin.ToString();

                //Carburant
                tbIDCarburantDesiderata.Text = pTmp.idCarburant.ToString();
                //C_CarburantVoiture CarbTmp = new G_CarburantVoiture(sConnex).Lire_ID(int.Parse(tbIdCarburant.Text));
                //cbNomCarburant.Text = CarbTmp.nomCarburant;

                //Couleur
                tbIDCouleurDesiderata.Text = pTmp.idCouleur.ToString();
                //C_CouleurVoiture CoulTmp = new G_CouleurVoiture(sConnex).Lire_ID(int.Parse(tbIdCouleur.Text));
                //cbNomCouleur.Text = CoulTmp.nomCouleur;

                //Kilométrage
                tbKilometrageDesiderata.Text = pTmp.kilometrageMax.ToString();
            }
            else MessageBox.Show("Sélectionner l'enregistrement à consulter");
        }

        private void bAjouter_Click(object sender, RoutedEventArgs e)
        {
            gestionEnabledBoutonMenu(false);
            ficheDataGridClients.IsEnabled = true;
        }

        private void bAnnuler_Click(object sender, RoutedEventArgs e)
        {
            gestionEnabledBoutonMenu(true);
            resetAllInfos();
            ficheDataGridClients.IsEnabled = false;
            ficheInfosDesiderata.IsEnabled = false;
        }

        private void gestionEnabledBoutonMenu(bool etat)
        {
            bAjouter.IsEnabled = etat;
            bConsulter.IsEnabled = etat;
            bEditer.IsEnabled = etat;
            bSupprimer.IsEnabled = etat;
            bAnnuler.IsEnabled = !etat;
            bQuitter.IsEnabled = etat;
        }

        private void remplirTBCBDesiderata()
        {
            RemplirCbMarque();
            RemplirCbModele();
            remplirCbCategorie();
            remplirCbCarburant();
            remplirCbCouleur();
        }

        private void resetAllInfos()
        {
            //Partie infos client
            tbID.Text = "";
            tbPre.Text = "";
            tbNom.Text = "";
            tbRue.Text = "";
            tbNumero.Text = "";
            tbBoite.Text = "";
            tbCodePo.Text = "";
            tbLocalite.Text = "";
            //Partie infos desiderata
            tbIDDesiderata.Text = "";
            tbIDClientsDesiderata.Text = "";
            tbIDMarqueDesiderata.Text = "";
            cbNomMarqueDesiderata.Text = "";
            tbIDModeleDesiderata.Text = "";
            cbNomModeleDesiderata.Text = "";
            tbIDCategorieDesiderata.Text = "";
            cbNomCategorieDesiderata.Text = "";
            tbAnneeFabrDesiderata.Text = "";
            tbIDCarburantDesiderata.Text = "";
            cbNomCarburantDesiderata.Text = "";
            tbIDCouleurDesiderata.Text = "";
            cbNomCouleurDesiderata.Text = "";
            tbKilometrageDesiderata.Text = "";
        }

        #region Remplissage ALL Combobox Desideratas
        private void RemplirCbMarque()
        {
            MarqueDataContext DCMarque = new MarqueDataContext();
            var requete = from marque in DCMarque.MarqueVoiture
                          select marque.nomMarque;

            foreach (var aa in requete)
            {
                cbNomMarqueDesiderata.Items.Add(aa.ToString());
            }
        }

        private void RemplirCbModele()
        {
            ModeleDataContext DCModele = new ModeleDataContext();
            var requete = from modele in DCModele.ModeleVoiture
                          select modele.nomModele;

            foreach (var aa in requete)
            {
                cbNomModeleDesiderata.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCategorie()
        {
            CategorieDataContext DCCategorie = new CategorieDataContext();
            var requete = from categorie in DCCategorie.CategorieVoiture
                          select categorie.nomCat;

            foreach (var aa in requete)
            {
                cbNomCategorieDesiderata.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCarburant()
        {
            CarburantDataContext DCCarburant = new CarburantDataContext();
            var requete = from carburant in DCCarburant.CarburantVoiture
                          select carburant.nomCarburant;

            foreach (var aa in requete)
            {
                cbNomCarburantDesiderata.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCouleur()
        {
            CouleurDataContext DCCouleur = new CouleurDataContext();
            var requete = from couleur in DCCouleur.CouleurVoiture
                          select couleur.nomCouleur;

            foreach (var aa in requete)
            {
                cbNomCouleurDesiderata.Items.Add(aa.ToString());
            }
        }
        #endregion

        #region EVENEMENTS SELECTIONCHANGED ALL CB

        private void cbNomMarqueDesiderata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarqueDataContext DCMarque = new MarqueDataContext();
            var requete = from marque in DCMarque.MarqueVoiture
                          where marque.nomMarque == cbNomMarqueDesiderata.SelectedItem.ToString()
                          select marque.idMarque;

            foreach (var aa in requete)
            {
                tbIDMarqueDesiderata.Text = aa.ToString();
            }
        }

        private void cbNomModeleDesiderata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModeleDataContext DCModele = new ModeleDataContext();
            var requete = from modele in DCModele.ModeleVoiture
                          where modele.nomModele == cbNomModeleDesiderata.SelectedItem.ToString()
                          select modele.idModele;

            foreach (var aa in requete)
            {
                tbIDModeleDesiderata.Text = aa.ToString();
            }
        }

        private void cbNomCategorieDesiderata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorieDataContext DCCategorie = new CategorieDataContext();
            var requete = from categorie in DCCategorie.CategorieVoiture
                          where categorie.nomCat == cbNomCategorieDesiderata.SelectedItem.ToString()
                          select categorie.idCat;

            foreach (var aa in requete)
            {
                tbIDCategorieDesiderata.Text = aa.ToString();
            }
        }

        private void cbNomCarburantDesiderata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarburantDataContext DCCarburant = new CarburantDataContext();
            var requete = from carburant in DCCarburant.CarburantVoiture
                          where carburant.nomCarburant == cbNomCarburantDesiderata.SelectedItem.ToString()
                          select carburant.idCarburant;

            foreach (var aa in requete)
            {
                tbIDCarburantDesiderata.Text = aa.ToString();
            }
        }

        private void cbNomCouleurDesiderata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CouleurDataContext DCCouleur = new CouleurDataContext();
            var requete = from couleur in DCCouleur.CouleurVoiture
                          where couleur.nomCouleur == cbNomCouleurDesiderata.SelectedItem.ToString()
                          select couleur.idCouleur;

            foreach (var aa in requete)
            {
                tbIDCouleurDesiderata.Text = aa.ToString();
            }
        }
        #endregion

        private void btnConfirmerAjoutDesiderata_Click(object sender, RoutedEventArgs e)
        {
            resetAllInfos();
        }
    }
}
