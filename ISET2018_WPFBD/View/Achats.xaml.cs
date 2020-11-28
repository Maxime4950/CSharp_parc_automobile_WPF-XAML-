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

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Achats.xaml
    /// </summary>
    public partial class Achats : Window
    {
        private string chConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";

        private ViewModel.VM_Achat LocalAchat;
        public Achats()
        {
            InitializeComponent();
            LocalAchat = new ViewModel.VM_Achat();
            DataContext = LocalAchat;
        }

        private void dgAchats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAchats.SelectedIndex >= 0) LocalAchat.AchatSelectionnee2UnAchat();
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dgAchats.SelectedIndex >= 0)
            {
                EtatEnabledBouton(false);
                FicheDetails.IsEnabled = true;
            }
        }

        private void bConsulterFacture_Click(object sender, RoutedEventArgs e)
        {
            rtbFactureAchats.Document.Blocks.Clear();

            C_ClientsVoiture clTmp = new G_ClientsVoiture(chConnexion).Lire_ID(int.Parse(tbIdClient.Text));
            C_StockVoiture voitTmp = new G_StockVoiture(chConnexion).Lire_ID(int.Parse(tbIdVoiture.Text));

            string nomFichier = clTmp.nomClient + "_" + clTmp.prenomClient + "_IDC" + tbIdClient.Text + "_IDV" + tbIdVoiture.Text + "_FactureAchat.txt";
            string nomRepertoire = @"C:\Users\Maesm\Documents\Complement_P\ISET2018_WPFBD_MVVM_concept\Factures_A";

            if (System.IO.File.Exists(nomRepertoire + "/" + nomFichier))
            {
                string[] lines = System.IO.File.ReadAllLines(nomRepertoire + "/" + nomFichier);

                for (int i = 0; i < lines.Length; i++)
                {
                    rtbFactureAchats.AppendText(lines[i] + "\n");
                }
            }
            else
            {
                rtbFactureAchats.AppendText("La facture n'est pas disponible.");
            }
        }

        private void bAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EtatEnabledBouton(true);
            FicheDetails.IsEnabled = false;
            rtbFactureAchats.Document.Blocks.Clear();
        }

        private void EtatEnabledBouton(bool etat)
        {
            bDetails.IsEnabled = etat;
            bConsulterFacture.IsEnabled = !etat;
            bSupprimer.IsEnabled = etat;
            bAnnuler.IsEnabled = !etat;
            bQuitter.IsEnabled = etat;
        }

    }
}
