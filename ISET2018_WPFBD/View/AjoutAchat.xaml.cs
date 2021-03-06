﻿using System;
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
using System.Collections.ObjectModel;
using ISET2018_WPFBD.DataAccess.DataObject;
using ISET2018_WPFBD.Classes;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutAchat.xaml
    /// </summary>
    public partial class AjoutAchat : Window
    {
        private ViewModel.VM_Personne LocalPersonne;
        private ViewModel.VM_Stock LocalStock;
        private ViewModel.VM_Achat LocalAchat;

        FactureAchat factA = new FactureAchat();

        private string sConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";

        JournalEvenements journal = new JournalEvenements();
        ListeClientsInteresses listeInteret = new ListeClientsInteresses();

        public AjoutAchat()
        {
            InitializeComponent();
            LocalPersonne = new ViewModel.VM_Personne();
            LocalStock = new ViewModel.VM_Stock();
            LocalAchat = new ViewModel.VM_Achat();

            ficheInfoDGAchat.DataContext = LocalAchat;
            ficheInfoClientA.DataContext = LocalPersonne;
            ficheInfoVoiture.DataContext = LocalStock;
 

            RemplirAllCb();
        }

        private void dgClientsAjoutAchat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientsAjoutAchat.SelectedIndex >= 0)
            {
                LocalPersonne.PersonneSelectionnee2UnePersonne();
                tbIDClientConf.Text = tbID.Text;
            }
        }

        private void btnConfirmerAchat_Click(object sender, RoutedEventArgs e)
        {
            if (tbIDClientConf.Text != "" && tbIDVoitureConf.Text != "" && tbPrix.Text != "" && dtpDate.Text != "" && cbPaiement.Text != "")
            {
                //Activation des panels info client et voiture et desactivation de la grid info achat
                EtatEnabledGrid(true);

                int iID = new G_AchatVente(sConnexion).Ajouter(int.Parse(tbIDVoitureConf.Text), int.Parse(tbIDClientConf.Text), int.Parse(tbPrix.Text)
                 , DateTime.Parse(dtpDate.Text), int.Parse(tbPaiementID.Text), "achat");
                factA.creerFactureAchat(tbIDClientConf, tbNom, tbPre, tbIDVoitureConf, cbMarque, cbModele,
                cbCategorie, tbAnneeFabr, cbCarburant, cbCouleur, tbKilometrage, tbPrix, dtpDate, tbPaiementID, cbPaiement);
                //Ajout au journal des évenements
                C_ClientsVoiture tmp = new G_ClientsVoiture(sConnexion).Lire_ID(int.Parse(tbIDClientConf.Text));
                journal.AjoutAchatJournal(tbIDVoitureConf, cbMarque, cbModele, tbIDClientConf, tmp.nomClient, tmp.prenomClient, dtpDate, tbPrix);
                //Création de la liste des personnes potentiellements interessés par le véhicule
                listeInteret.creerListeClientsInt(tbIDVoitureConf, sConnexion);
                //Pour vider les textbox
                AnnulerInfo(); 
                ActualiserDataGridAchat();
                MessageBox.Show("Achat N° : " + iID.ToString() + " effectué");
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs ! ");
            }
        }

        private void EtatEnabledGrid(bool etat)
        {
            ficheInfoClientA.IsEnabled = etat;
            ficheInfoVoiture.IsEnabled = etat;
            ficheInfoAchat.IsEnabled = !etat;
        }

        private void btnAnnulerAchat_Click(object sender, RoutedEventArgs e)
        {
            //Supprime la voiture qui avait été ajoutée précédement au stock si on annule car par encore achetée.
            if(tbIDVoitureConf.Text != "")
            {
                new G_StockVoiture(sConnexion).Supprimer(int.Parse(tbIDVoitureConf.Text));
            }
            AnnulerInfo();
        }

        

        private void AnnulerInfo()
        {
            tbIDVoitureConf.Text = "";
            tbIDClientConf.Text = "";
            dtpDate.Text = "";
            tbPrix.Text = "";
            //Activation des panels info client et voiture et desactivation de la grid info achat
            EtatEnabledGrid(true);
        }

        private void ActualiserDataGridAchat()
        {
            LocalAchat = new ViewModel.VM_Achat();
            ficheInfoDGAchat.DataContext = LocalAchat;
        }

        #region Fonction remplissage Combobox
        private void RemplirAllCb()
        {
            RemplirCbMarque();
            RemplirCbModele();
            remplirCbCategorie();
            remplirCbCarburant();
            remplirCbCouleur();
            remplirCbPaiement();
        }

        private void RemplirCbMarque()
        {
            MarqueDataContext DCMarque = new MarqueDataContext();
            var requete = from marque in DCMarque.MarqueVoiture
                          select marque.nomMarque;

            foreach (var aa in requete)
            {
                cbMarque.Items.Add(aa.ToString());
            }
        }

        private void RemplirCbModele()
        {
            ModeleDataContext DCModele = new ModeleDataContext();
            var requete = from modele in DCModele.ModeleVoiture
                          select modele.nomModele;

            foreach (var aa in requete)
            {
                cbModele.Items.Add(aa.ToString());
            }
        }

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

        private void remplirCbCategorie()
        {
            CategorieDataContext DCCategorie = new CategorieDataContext();
            var requete = from categorie in DCCategorie.CategorieVoiture
                          select categorie.nomCat;

            foreach (var aa in requete)
            {
                cbCategorie.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCarburant()
        {
            CarburantDataContext DCCarburant = new CarburantDataContext();
            var requete = from carburant in DCCarburant.CarburantVoiture
                          select carburant.nomCarburant;

            foreach (var aa in requete)
            {
                cbCarburant.Items.Add(aa.ToString());
            }
        }

        private void remplirCbCouleur()
        {
            CouleurDataContext DCCouleur = new CouleurDataContext();
            var requete = from couleur in DCCouleur.CouleurVoiture
                          select couleur.nomCouleur;

            foreach (var aa in requete)
            {
                cbCouleur.Items.Add(aa.ToString());
            }
        }
        #endregion

        #region Selection changed combobox

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

        private void cbMarque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarqueDataContext DCMarque = new MarqueDataContext();
            var requete = from marque in DCMarque.MarqueVoiture
                          where marque.nomMarque == cbMarque.SelectedItem.ToString()
                          select marque.idMarque;

            foreach (var aa in requete)
            {
                tbIDMarque.Text = aa.ToString();
            }
        }

        private void cbModele_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModeleDataContext DCModele = new ModeleDataContext();
            var requete = from modele in DCModele.ModeleVoiture
                          where modele.nomModele == cbModele.SelectedItem.ToString()
                          select modele.idModele;

            foreach (var aa in requete)
            {
                tbIDModele.Text = aa.ToString();
            }
        }

        private void btnAjouterFrais_Click(object sender, RoutedEventArgs e)
        {
            View.FraisAchat f = new View.FraisAchat();
            f.tbIDVoiture.Text = tbIDVoitureConf.Text;
            f.ShowDialog();
        }

        private void cbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorieDataContext DCCategorie = new CategorieDataContext();
            var requete = from categorie in DCCategorie.CategorieVoiture
                          where categorie.nomCat == cbCategorie.SelectedItem.ToString()
                          select categorie.idCat;

            foreach (var aa in requete)
            {
                tbCategorie.Text = aa.ToString();
            }
        }

        private void cbCarburant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarburantDataContext DCCarburant = new CarburantDataContext();
            var requete = from carburant in DCCarburant.CarburantVoiture
                          where carburant.nomCarburant == cbCarburant.SelectedItem.ToString()
                          select carburant.idCarburant;

            foreach (var aa in requete)
            {
                tbIDCarburant.Text = aa.ToString();
            }
        }

        private void cbCouleur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CouleurDataContext DCCouleur = new CouleurDataContext();
            var requete = from couleur in DCCouleur.CouleurVoiture
                          where couleur.nomCouleur == cbCouleur.SelectedItem.ToString()
                          select couleur.idCouleur;

            foreach (var aa in requete)
            {
                tbIDCouleur.Text = aa.ToString();
            }
        }

        #endregion

        private void btnConfirmerVoiture_Click(object sender, RoutedEventArgs e)
        {
            if (tbIDMarque.Text != "" && tbIDModele.Text != "" && tbCategorie.Text != "" && tbAnneeFabr.Text != "" && tbKilometrage.Text != "" && tbIDCarburant.Text != "" && tbIDCouleur.Text != "")
            {
                //Désactivation des panels info client et voiture et activation de la grid info achat
                EtatEnabledGrid(false); 

                tbIDClientConf.Text = tbID.Text;

                //Ajout de la voiture dans le stock de la BD

                int iIDVoit = new G_StockVoiture(sConnexion).Ajouter(int.Parse(tbIDMarque.Text), int.Parse(tbIDModele.Text), int.Parse(tbCategorie.Text)
                    , int.Parse(tbAnneeFabr.Text), int.Parse(tbIDCarburant.Text), int.Parse(tbIDCouleur.Text), int.Parse(tbKilometrage.Text));

                tbIDVoitureConf.Text = iIDVoit.ToString();
            }
            else
            {
                MessageBox.Show("Veuillez remplir toutes les infos du véhicule");
            }
        }

        private void dgAchatAjoutAchat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAchatAjoutAchat.SelectedIndex >= 0)
            {
                LocalAchat.AchatSelectionnee2UnAchat();
            }
        }
    }
}
