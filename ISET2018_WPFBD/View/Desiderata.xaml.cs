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
using System.Text.RegularExpressions;
using ISET2018_WPFBD.DataAccess.DataObject;
using ISET2018_WPFBD.Model;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Desiderata.xaml
    /// </summary>
    public partial class Desiderata : Window
    {
        private ViewModel.VM_Desideratas LocalDesiderata;
        private string chConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        public Desiderata()
        {
            InitializeComponent();
            LocalDesiderata = new ViewModel.VM_Desideratas();
            DataContext = LocalDesiderata;

            //Remplissage des combobox
            remplirCB();
        }

        private void dgDesideratas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDesideratas.SelectedIndex >= 0) LocalDesiderata.DesiderataSelectionne2UnDesiderata();
        }

        private void remplirCB()
        {
            RemplirCbClient();
            RemplirCbMarque();
            RemplirCbModele();
            remplirCbCategorie();
            remplirCbCarburant();
            remplirCbCouleur();
        }

        #region Remplissage ALL Combobox Desideratas
        private void RemplirCbClient()
        {
            ClientDataContext DCClient = new ClientDataContext();
            var requete = from client in DCClient.ClientsVoiture
                          select client;

            foreach (var aa in requete)
            {
                cbNomClient.Items.Add(aa.nomClient.ToString() + " " + aa.prenomClient.ToString() + " " +aa.idClient.ToString());
            }
        }
        private void RemplirCbMarque()
        {
            MarqueDataContext DCMarque = new MarqueDataContext();
            var requete = from marque in DCMarque.MarqueVoiture
                          select marque.nomMarque;

            foreach (var aa in requete)
            {
                cbNomMarque.Items.Add(aa.ToString());
            }
        }

        private void RemplirCbModele()
        {
            ModeleDataContext DCModele = new ModeleDataContext();
            var requete = from modele in DCModele.ModeleVoiture
                          select modele.nomModele;

            foreach (var aa in requete)
            {
                cbNomModele.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCategorie()
        {
            CategorieDataContext DCCategorie = new CategorieDataContext();
            var requete = from categorie in DCCategorie.CategorieVoiture
                          select categorie.nomCat;

            foreach (var aa in requete)
            {
                cbNomCategorie.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCarburant()
        {
            CarburantDataContext DCCarburant = new CarburantDataContext();
            var requete = from carburant in DCCarburant.CarburantVoiture
                          select carburant.nomCarburant;

            foreach (var aa in requete)
            {
                cbNomCarburant.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCouleur()
        {
            CouleurDataContext DCCouleur = new CouleurDataContext();
            var requete = from couleur in DCCouleur.CouleurVoiture
                          select couleur.nomCouleur;

            foreach (var aa in requete)
            {
                cbNomCouleur.Items.Add(aa.ToString());
            }
        }

        #endregion

        #region Action au changement d'item selectionné dans les combobox

        private void cbNomClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbNomClient.Text != "")
            {
                string[] test = cbNomClient.Items.GetItemAt(cbNomClient.SelectedIndex).ToString().Split(' ');
                tbIDClient.Text = test[2].ToString();
                LocalDesiderata.UnDesiderata.IDClient = int.Parse(tbIDClient.Text);
            }
        }

        private void cbNomMarque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbNomMarque.Text != "")
            {
                MarqueDataContext DCMarque = new MarqueDataContext();
                var requete = from marque in DCMarque.MarqueVoiture
                              where marque.nomMarque == cbNomMarque.SelectedItem.ToString()
                              select marque.idMarque;

                foreach (var aa in requete)
                {
                    tbIDMarque.Text = aa.ToString();
                    LocalDesiderata.UnDesiderata.IDMarque = int.Parse(tbIDMarque.Text);
                }
            }
        }

        private void cbNomModele_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModeleDataContext DCModele = new ModeleDataContext();
            var requete = from modele in DCModele.ModeleVoiture
                          where modele.nomModele == cbNomModele.SelectedItem.ToString()
                          select modele.idModele;

            foreach (var aa in requete)
            {
                tbIDModele.Text = aa.ToString();
                LocalDesiderata.UnDesiderata.IDModele = int.Parse(tbIDModele.Text);
            }
        }

        private void cbNomCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorieDataContext DCCategorie = new CategorieDataContext();
            var requete = from categorie in DCCategorie.CategorieVoiture
                          where categorie.nomCat == cbNomCategorie.SelectedItem.ToString()
                          select categorie.idCat;

            foreach (var aa in requete)
            {
                tbIDCategorie.Text = aa.ToString();
                LocalDesiderata.UnDesiderata.IDCat = int.Parse(tbIDCategorie.Text);
            }
        }

        private void cbNomCarburant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarburantDataContext DCCarburant = new CarburantDataContext();
            var requete = from carburant in DCCarburant.CarburantVoiture
                          where carburant.nomCarburant == cbNomCarburant.SelectedItem.ToString()
                          select carburant.idCarburant;

            foreach (var aa in requete)
            {
                tbIDCarburant.Text = aa.ToString();
                LocalDesiderata.UnDesiderata.IDCarburant = int.Parse(tbIDCarburant.Text);
            }
        }

        private void cbNomCouleur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CouleurDataContext DCCouleur = new CouleurDataContext();
            var requete = from couleur in DCCouleur.CouleurVoiture
                          where couleur.nomCouleur == cbNomCouleur.SelectedItem.ToString()
                          select couleur.idCouleur;

            foreach (var aa in requete)
            {
                tbIDCouleur.Text = aa.ToString();
                LocalDesiderata.UnDesiderata.IDCouleur = int.Parse(tbIDCouleur.Text);
            }
        }
        #endregion

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void bEditer_Click(object sender, RoutedEventArgs e)
        {
            //Client
            C_ClientsVoiture ClientTmp = new G_ClientsVoiture(chConnexion).Lire_ID(int.Parse(tbIDClient.Text));
            cbNomClient.Text = ClientTmp.nomClient + " " + ClientTmp.prenomClient + " " + ClientTmp.idClient;

            //Marque
            C_Marque MarqueTmp = new G_Marque(chConnexion).Lire_ID(int.Parse(tbIDMarque.Text));
            cbNomMarque.Text = MarqueTmp.nomMarque;

            //Modele
            C_Modele ModeleTmp = new G_Modele(chConnexion).Lire_ID(int.Parse(tbIDModele.Text));
            cbNomModele.Text = ModeleTmp.nomModele;

            //Catégorie
            C_Categorie CatTmp = new G_Categorie(chConnexion).Lire_ID(int.Parse(tbIDCategorie.Text));
            cbNomCategorie.Text = CatTmp.nomCat;

            //Carburant
            C_Carburant CarbTmp = new G_Carburant(chConnexion).Lire_ID(int.Parse(tbIDCarburant.Text));
            cbNomCarburant.Text = CarbTmp.nomCarburant;

            //Couleur
            C_Couleur CoulTmp = new G_Couleur(chConnexion).Lire_ID(int.Parse(tbIDCouleur.Text));
            cbNomCouleur.Text = CoulTmp.nomCouleur;
        }
    }
}
