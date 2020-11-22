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
using System.Collections.ObjectModel;

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

        private string sConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Maesm\Documents\Complement_P\ISET2018_WPFBD_MVVM_concept\ISET2018_WPFBD\BD_Voiture_mvvm.mdf;Integrated Security=True;Connect Timeout=30";

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


            //Ajout des id de paiements
            for (int i = 0; i < LocalPaiement.BcpPaiement.Count(); i++)
            {
                cbPaiementID.Items.Add(LocalPaiement.BcpPaiement[i].idPaiement);
            }

            //Ajout des modes de paiements
            for (int i = 0; i < LocalPaiement.BcpPaiement.Count(); i++)
            {
                cbPaiement.Items.Add(LocalPaiement.BcpPaiement[i].nomPaiement);
            }

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
                 , DateTime.Parse(dtpDate.Text), int.Parse(cbPaiementID.Text), "vente");
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
            cbPaiementID.Text = "";
            cbPaiement.Text = "";
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
        }

        private void RemplirCbMarque()
        {
            for (int i = 0; i < LocalMarque.BcpMarques.Count(); i++)
            {
                cbMarque.Items.Add(LocalMarque.BcpMarques[i].nomMarque);
            }
        }

        private void RemplirCbModele()
        {
            for (int i = 0; i < LocalModele.BcpModeles.Count(); i++)
            {
                cbModele.Items.Add(LocalModele.BcpModeles[i].nomModele);
            }
        }
    }
}
