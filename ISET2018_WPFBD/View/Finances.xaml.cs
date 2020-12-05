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
using ISET2018_WPFBD.DataAccess.DataObject;
using ISET2018_WPFBD.Classes;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Finances.xaml
    /// </summary>
    public partial class Finances : Window
    {
        JournalEvenements journal = new JournalEvenements();
        public Finances()
        {
            InitializeComponent();
            JournalEvenements journal = new JournalEvenements();
            journal.LireContenuJournal(rtbJournalEvenements);
            dtpDateFin.DisplayDate = DateTime.Today;
        }

        public void afficherDonneesFinances()
        {
            rtbAchats.Document.Blocks.Clear();
            rtbVentes.Document.Blocks.Clear();
            DateTime dateDebut = Convert.ToDateTime(dtpDateDebut.SelectedDate);
            DateTime dateFin = Convert.ToDateTime(dtpDateFin.SelectedDate);

            int totAchat = 0;
            int totVentes = 0;
            AchatVenteDataContext DCFinances = new AchatVenteDataContext();
            //Requete pour l'achat
            var requete = from finances in DCFinances.AchatVenteVoiture
                          where DateTime.Compare(finances.dateOperation, dateDebut) >= 0
                          && DateTime.Compare(finances.dateOperation, dateFin) <= 0  //Si la date est plus grand ou égale à la date de début et plus petite ou égale à la date de fin
                          && finances.typeOperation == "achat"
                          select finances.prixOperation;

            //Requete pour la vente
            var requete2 = from finances in DCFinances.AchatVenteVoiture
                           where DateTime.Compare(finances.dateOperation, dateDebut) >= 0
                           && DateTime.Compare(finances.dateOperation, dateFin) <= 0  //Si la date est plus grand ou égale à la date de début et plus petite ou égale à la date de fin
                           && finances.typeOperation == "vente"
                           select finances.prixOperation;


            foreach (var aa in requete)
            {
                rtbAchats.AppendText("- " + aa.ToString() + "€\n");
                totAchat += int.Parse(aa.ToString());
            }

            foreach (var aa in requete2)
            {
                rtbVentes.AppendText("+ " + aa.ToString() + "€\n");
                totVentes += int.Parse(aa.ToString());
            }


            if (totAchat < totVentes)
            {
                int benefice = totVentes - totAchat;
                lblBeneficePerte.Content = ("B E N E F I C E  D E : " + benefice + " €\n" +
                    "Sur la période du " + dateDebut.ToLongDateString() + "  au  " + dateFin.ToLongDateString());
            }
            else if (totAchat > totVentes)
            {
                int perte = totVentes - totAchat;
                lblBeneficePerte.Content = ("P E R T E  D E : " + perte + " €\n" +
                    "Sur la période du " + dateDebut.ToLongDateString() + "  au  " + dateFin.ToLongDateString());
            }
            else
            {
                lblBeneficePerte.Content = ("B E N E F I C E / P E R T E  D E : 0 €\n" +
                   "Sur la période du " + dateDebut.ToLongDateString() + "  au  " + dateFin.ToLongDateString());
            }

        }

        private void dtpDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EmpecherDepassementDateFinDebut();
            afficherDonneesFinances();
        }

        private void dtpDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EmpecherDepassementDateFinDebut();
            afficherDonneesFinances();
        }

        private void EmpecherDepassementDateFinDebut()
        {
            DateTime dateDebut = Convert.ToDateTime(dtpDateDebut.SelectedDate);
            DateTime dateFin = Convert.ToDateTime(dtpDateFin.SelectedDate);

            if (DateTime.Compare(dateDebut, dateFin) >= 0) //Si la date de début est plus grande que la date de fin alors on doit adapter
            {
                dtpDateDebut.SelectedDate = DateTime.Today.AddDays(-1);
                dtpDateFin.SelectedDate = DateTime.Today;
            }
            else if (DateTime.Compare(dateFin, DateTime.Today) >= 0) //Pour empecher d'aller plus loin que la date du jour car de toute facon aucun chiffre
            {
                dtpDateFin.SelectedDate = DateTime.Today;
            }
        }
    }
}
