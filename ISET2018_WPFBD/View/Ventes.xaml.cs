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
using System.IO;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour AchatVente.xaml
    /// </summary>
    public partial class Vente : Window
    {
        #region Initialisation des variables
        private ViewModel.VM_Vente LocalVente;
        private string chConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        #endregion

        #region Constructeur Vente
        public Vente()
        {
            InitializeComponent();
            LocalVente = new ViewModel.VM_Vente();
            DataContext = LocalVente;
        }
        #endregion

        #region Méthodes

        private void dgVentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVentes.SelectedIndex >= 0) LocalVente.VenteSelectionnee2UneVente();
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bConsulterFacture_Click(object sender, RoutedEventArgs e)
        {
            rtbFactureVentes.Document.Blocks.Clear();

            C_ClientsVoiture clTmp = new G_ClientsVoiture(chConnexion).Lire_ID(int.Parse(tbIdClient.Text));
            C_StockVoiture voitTmp = new G_StockVoiture(chConnexion).Lire_ID(int.Parse(tbIdVoiture.Text));

            string nomFichier = clTmp.nomClient + "_" + clTmp.prenomClient + "_IDC" + tbIdClient.Text + "_IDV" + tbIdVoiture.Text + "_FactureVente.txt";
            string nomRepertoire = @"C:\Users\Maesm\Documents\Complement_P\ISET2018_WPFBD_MVVM_concept\Factures_V";

            if (System.IO.File.Exists(nomRepertoire + "/" + nomFichier))
            {
                string[] lines = System.IO.File.ReadAllLines(nomRepertoire + "/" + nomFichier);

                for (int i = 0; i < lines.Length; i++)
                {
                    rtbFactureVentes.AppendText(lines[i] + "\n");
                }
            }
            else
            {
                rtbFactureVentes.AppendText("La facture n'est pas disponible.");
            }
        }

        private void bDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dgVentes.SelectedIndex >= 0)
            {
                EtatEnabledBouton(false);
                FicheDetails.IsEnabled = true;
            }
        }

        private void bAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EtatEnabledBouton(true);
            FicheDetails.IsEnabled = false;
            rtbFactureVentes.Document.Blocks.Clear();
        }

        private void EtatEnabledBouton(bool etat)
        {
            bDetails.IsEnabled = etat;
            bConsulterFacture.IsEnabled = !etat;
            bSupprimer.IsEnabled = etat;
            bAnnuler.IsEnabled = !etat;
            bQuitter.IsEnabled = etat;
        }
        #endregion$
    }
}
