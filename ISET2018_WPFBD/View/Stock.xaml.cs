using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ISET2018_WPFBD.Model;
using ISET2018_WPFBD.DataAccess.DataObject;
using SautinSoft.Document;

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Stock.xaml
    /// </summary>
    public partial class Stock : Window
    {
        private ViewModel.VM_Stock LocalStock;

        private string sConnex = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        public Stock()
        {
            InitializeComponent();
            LocalStock = new ViewModel.VM_Stock();
            DataContext = LocalStock;
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStock.SelectedIndex >= 0) LocalStock.StockSelectionnee2UnStock();
        }

        private void bConsulter_Click(object sender, RoutedEventArgs e)
        {
            FicheInfoStock.IsEnabled = true;
        }

        private void bConsulterStockHtml_Click(object sender, RoutedEventArgs e)
        {
            

            DocumentCore doc = new DocumentCore();
            doc.Content.End.Insert("Page HTML Stock\n\n", new CharacterFormat() { FontName = "Verdana" });
          

            StockDataContext DCStock = new StockDataContext();
            var requete = from stock in DCStock.StockVoiture
                          select stock;

            foreach (var aa in requete)
            {
                C_Marque marqueTmp = new G_Marque(sConnex).Lire_ID(aa.idMarque);
                C_Modele modeleTmp = new G_Modele(sConnex).Lire_ID(aa.idModele);
                C_Categorie catTmp = new G_Categorie(sConnex).Lire_ID(aa.idCategorie);
                C_Carburant carbuTmp = new G_Carburant(sConnex).Lire_ID((int)aa.idCarburant); //cast car int?
                C_Couleur coulTmp = new G_Couleur(sConnex).Lire_ID((int)aa.idCouleur);

                doc.Content.End.Insert("ID Véhicule :" + aa.idVoiture +"\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("ID Marque :" + aa.idMarque +  "\tNom Marque : " + marqueTmp.nomMarque +"\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("ID Modèle :" + aa.idModele + "\tNom Modele : " + modeleTmp.nomModele + "\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("ID Catégorie :" + aa.idCategorie + "\tNom Catégorie : " + catTmp.nomCat + "\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("Année de fabrication :" + aa.anneeFabrication + "\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("ID Carburant :" + aa.idCarburant + "\tNom Carburant : " + carbuTmp.nomCarburant + "\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("ID Couleur :" + aa.idCouleur + "\tNom Couleur : " + coulTmp.nomCouleur + "\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("ID Kilometrage :" + aa.kilometrage + "\n", new CharacterFormat() { FontName = "Verdana" });
                doc.Content.End.Insert("----------------------------------------------------------------------\n", new CharacterFormat() { FontName = "Verdana" });
            }

            doc.Save("Stock.html");

            //Lancement du navigateur

            View.Navigateur f = new View.Navigateur();
            f.ShowDialog();
        }

        private void bListe_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
