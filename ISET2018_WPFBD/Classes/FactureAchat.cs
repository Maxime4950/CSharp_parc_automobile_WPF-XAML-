using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO; //fichier
using ISET2018_WPFBD.DataAccess.DataObject;

namespace ISET2018_WPFBD.Classes
{
    class FactureAchat
    {
        #region Initialisaton des variables
        int prixTotalAchat = 0; //Pour les finances
        int sommeFrais = 0;
        int cptNbFrais = 0;
        #endregion

        #region Constructeur Facture Achat
        public FactureAchat()
        {

        }
        #endregion

        #region Méthodes

        public void creerFactureAchat(TextBox tbIdClientConfirme, TextBox tbIdNomClient, TextBox tbIdPrenomClient,
            TextBox tbIdVoitureConfirme, ComboBox cbNomMarque, ComboBox cbNomModele, ComboBox cbNomCategorie, TextBox tbAnneeFabricationStock
            , ComboBox cbNomCarburant, ComboBox cbNomCouleur, TextBox tbKilometrage, TextBox tbPrix, DatePicker dtpDate, TextBox tbIdPaiement,
           ComboBox cbNomPaiement)
        {
            string nomFichier = tbIdNomClient.Text + "_" + tbIdPrenomClient.Text + "_IDC" + tbIdClientConfirme.Text + "_IDV" + tbIdVoitureConfirme.Text + "_FactureAchat.txt";
            string nomRepertoire = @"C:\Users\Maesm\Documents\Complement_P\ISET2018_WPFBD_MVVM_concept\Factures_A";

            // If directory does not exist, create it. 

            if (!Directory.Exists(nomRepertoire)) //Si le repertoie n'existe pas on le crée
            {

                Directory.CreateDirectory(nomRepertoire);

                //Ensuite on peut créer la facture

                using (StreamWriter factA = File.CreateText(nomRepertoire + "/" + nomFichier))
                // + 
                {
                    factA.WriteLine("\t\t\t\t[FACTURE ACHAT]");
                    factA.WriteLine("\t\t\t\t---------------\n");

                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                            INFORMATIONS DU CLIENT                            |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    factA.WriteLine("\n[ID Client] : " + tbIdClientConfirme.Text
                        + "\t[Nom] : " + tbIdNomClient.Text
                        + "\t\t[Prénom] : " + tbIdPrenomClient.Text
                        + "\n");

                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                           INFORMATIONS DE LA VOITURE                         |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    factA.WriteLine("\n[ID Voiture] : " + tbIdVoitureConfirme.Text
                                    + "\t\t[Marque] : " + cbNomMarque.Text
                                    + "\t\t\t[Modele] : " + cbNomModele.Text
                                    + "\n[Categorie] : " + cbNomCategorie.Text
                                    + "\t[Année de fabrication] : " + tbAnneeFabricationStock.Text
                                    + "\n[Carburant] : " + cbNomCarburant.Text
                                    + "\t[Couleur] : " + cbNomCouleur.Text
                                    + "\n[Kilométrage] : " + tbKilometrage.Text);


                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                           DETAILS DE L'ACHAT                                 |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    factA.WriteLine("\n[Prix] : " + tbPrix.Text + " €"
                        + "\t\t[Date de vente] : " + dtpDate.Text
                        + "\n[Id Paiement] : " + tbIdPaiement.Text
                        + "\t\t[Nom Paiement] : " + cbNomPaiement.Text
                        + "\n");

                    factA.Close();
                }

            }
            else
            {
                using (StreamWriter factA = File.CreateText(nomRepertoire + "/" + nomFichier))
                // + 
                {
                    factA.WriteLine("\t\t\t\t[FACTURE ACHAT]");
                    factA.WriteLine("\t\t\t\t---------------\n");

                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                            INFORMATIONS DU CLIENT                            |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    factA.WriteLine("\n[ID Client] : " + tbIdClientConfirme.Text
                        + "\t[Nom] : " + tbIdNomClient.Text
                        + "\t\t[Prénom] : " + tbIdPrenomClient.Text
                        + "\n");

                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                           INFORMATIONS DE LA VOITURE                         |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    factA.WriteLine("\n[ID Voiture] : " + tbIdVoitureConfirme.Text
                                    + "\t\t[Marque] : " + cbNomMarque.Text
                                    + "\t\t\t[Modele] : " + cbNomModele.Text
                                    + "\n[Categorie] : " + cbNomCategorie.Text
                                    + "\t[Année de fabrication] : " + tbAnneeFabricationStock.Text
                                    + "\n[Carburant] : " + cbNomCarburant.Text
                                    + "\t[Couleur] : " + cbNomCouleur.Text
                                    + "\n[Kilométrage] : " + tbKilometrage.Text);


                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                           DETAILS DE L'ACHAT                                 |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    factA.WriteLine("\n[Prix] : " + tbPrix.Text + " €"
                        + "\t\t[Date de vente] : " + dtpDate.Text
                        + "\n[Id Paiement] : " + tbIdPaiement.Text
                        + "\t\t[Nom Paiement] : " + cbNomPaiement.Text
                        + "\n");

                    


                    factA.WriteLine(" ______________________________________________________________________________");
                    factA.WriteLine("|                                                                              |");
                    factA.WriteLine("|                           DETAILS DES FRAIS                                  |");
                    factA.WriteLine("|______________________________________________________________________________|\n");

                    FraisDataContext DCFrais = new FraisDataContext();
                    var requete = from frais in DCFrais.FraisVoiture
                                  where frais.idVoiture == int.Parse(tbIdVoitureConfirme.Text)
                                  select frais;

                    foreach (var aa in requete)
                    {
                        cptNbFrais++;
                        factA.WriteLine("\n[N° : " + cptNbFrais.ToString() + "]");
                        factA.WriteLine("\n[ID Frais] : " + "Numéro : "+ aa.idFrais.ToString() 
                        + "\t\t[Nom Frais] : " + aa.nomFrais.ToString()
                        + "\n[Description Frais] : " + aa.descriptionFrais.ToString()
                        + "\t\t[Cout Frais] : " + aa.coutFrais.ToString() + " €"
                        + "\n") ;
                        sommeFrais += aa.coutFrais;
                    }

                    //Pour la gestion des finances
                    prixTotalAchat += int.Parse(tbPrix.Text) + sommeFrais;

                    factA.WriteLine("\nMONTANT TOTAL DE LA FACTURE : " + prixTotalAchat.ToString() + " €");

                    factA.Close();
                }
            }
        }
        #endregion
    }
}
