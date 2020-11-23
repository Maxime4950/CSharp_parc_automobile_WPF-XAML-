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

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutAchat.xaml
    /// </summary>
    public partial class AjoutAchat : Window
    {
        private ViewModel.VM_Personne LocalPersonne;
        private ViewModel.VM_Stock LocalStock;
        private ViewModel.VM_Vente LocalVente;
        private ViewModel.VM_Vente LocalVenteActualiser;
        private ViewModel.VM_Paiement LocalPaiement;

        //Pour les combobox
        private ViewModel.VM_Marques LocalMarque;
        private ViewModel.VM_Modeles LocalModele;

        private string sConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";

        public AjoutAchat()
        {
            InitializeComponent();
            LocalPersonne = new ViewModel.VM_Personne();
            LocalStock = new ViewModel.VM_Stock();
            LocalVente = new ViewModel.VM_Vente();
            LocalPaiement = new ViewModel.VM_Paiement();

            LocalMarque = new ViewModel.VM_Marques();
            LocalModele = new ViewModel.VM_Modeles();

            ficheInfoVentesA.DataContext = LocalVente;
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

        private void dgVentesAjoutAchat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVentesAjoutAchat.SelectedIndex >= 0)
            {
                LocalVente.VenteSelectionnee2UneVente();
            }
        }

        private void btnConfirmerAchat_Click(object sender, RoutedEventArgs e)
        {
            if (tbIDClientConf.Text != "" && tbIDVoitureConf.Text != "" && tbPrix.Text != "" && dtpDate.Text != "" && cbPaiement.Text != "")
            {
                int iID = new G_AchatVente(sConnexion).Ajouter(int.Parse(tbIDVoitureConf.Text), int.Parse(tbIDClientConf.Text), int.Parse(tbPrix.Text)
                 , DateTime.Parse(dtpDate.Text), int.Parse(tbPaiementID.Text), "vente");
                AnnulerInfo(); //Pour vider les textbox
                ActualiserDataGridVentes();
                MessageBox.Show("Vente effectuée N° : " + iID.ToString() + " effectuée");
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs ! ");
            }
        }

        private void btnAnnulerAchat_Click(object sender, RoutedEventArgs e)
        {
            AnnulerInfo();
        }

        

        private void AnnulerInfo()
        {
            tbIDVoitureConf.Text = "";
            tbIDClientConf.Text = "";
            dtpDate.Text = "";
            tbPrix.Text = "";
        }

        private void ActualiserDataGridVentes()
        {
            LocalVenteActualiser = new ViewModel.VM_Vente();
            ficheInfoVentesA.DataContext = LocalVenteActualiser;
        }

        private void RemplirAllCb()
        {
            RemplirCbMarque();
            RemplirCbModele();
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
    }
}
