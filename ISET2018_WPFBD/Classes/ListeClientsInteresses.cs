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
using ISET2018_WPFBD.Model;
using ISET2018_WPFBD.DataAccess.DataObject;

namespace ISET2018_WPFBD.Classes
{
    public class ListeClientsInteresses
    {
        #region Initialisaton des variables
   
        #endregion

        #region Constructeur Liste des clients interessés
        public ListeClientsInteresses()
        {

        }
        #endregion

        #region Méthodes

        public void creerListeClientsInt(TextBox tbIdVoiture, string chConnexion)
        {
            string nomFichier = "Voiture_"+ tbIdVoiture.Text + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_ClientsInteresses.txt"; //Attention si datetime.now.tostring s'écrit avec des / et pasd des _
            string nomRepertoire = @"C:\Users\Maesm\Documents\Complement_P\ISET2018_WPFBD_MVVM_concept\CliInteret";

            //Accès aux données
            C_StockVoiture voitTmp = new G_StockVoiture(chConnexion).Lire_ID(int.Parse(tbIdVoiture.Text));
            C_Marque marqueTmp = new G_Marque(chConnexion).Lire_ID(voitTmp.idMarque);
            C_Modele modeleTmp = new G_Modele(chConnexion).Lire_ID(voitTmp.idModele);
            C_Categorie catTmp = new G_Categorie(chConnexion).Lire_ID(voitTmp.idCategorie);
            C_Carburant carbuTmp = new G_Carburant(chConnexion).Lire_ID((int)voitTmp.idCarburant); //cast car int?
            C_Couleur coulTmp = new G_Couleur(chConnexion).Lire_ID((int)voitTmp.idCouleur);

            if (!Directory.Exists(nomRepertoire)) //Si le repertoie n'existe pas on le crée
            {
                // If directory does not exist, create it. 
                Directory.CreateDirectory(nomRepertoire);

                //Ensuite on peut créer le fichier

                using (StreamWriter fic = File.CreateText(nomRepertoire + "/" + nomFichier))
                {
                    fic.WriteLine("\t\t\t\t[LISTE DES CLIENTS INTERESSES]");
                    fic.WriteLine("\t\t\t\t---------------\n");

                    fic.WriteLine(" ______________________________________________________________________________");
                    fic.WriteLine("|                                                                              |");
                    fic.WriteLine("|                            INFORMATIONS DU VEHICULE                          |");
                    fic.WriteLine("|______________________________________________________________________________|\n");

                    fic.WriteLine("\n[ID Voiture] : " + tbIdVoiture.Text
                        + "\t[Nom Marque] : " + marqueTmp.nomMarque.ToString()
                        + "\t\t[Nom Modèle] : " + modeleTmp.nomModele.ToString()
                        + "\t\t[Nom Categorie] : " + catTmp.nomCat.ToString()
                        + "\t\t[Annee de fabrication] : " + voitTmp.anneeFabrication.ToString()
                        + "\t\t[Carburant] : " + carbuTmp.nomCarburant.ToString()
                        + "\t\t[Couleur] : " + coulTmp.nomCouleur.ToString()
                        + "\t\t[Kilométrage] : " + voitTmp.kilometrage.ToString()
                        + "\n");

                    fic.WriteLine(" ______________________________________________________________________________");
                    fic.WriteLine("|                                                                              |");
                    fic.WriteLine("|                            LISTE DES CLIENTS                                 |");
                    fic.WriteLine("|______________________________________________________________________________|\n");

                    DesideratasDataContext DCDesideratas = new DesideratasDataContext();
                    var requete = from desideratas in DCDesideratas.DesideratasVoiture
                                  where desideratas.idMarque == voitTmp.idMarque && desideratas.idModele == voitTmp.idModele
                                  select desideratas.idClient;

                    foreach (var aa in requete)
                    {
                        C_ClientsVoiture clientTmp = new G_ClientsVoiture(chConnexion).Lire_ID(aa);
                        fic.WriteLine("\n[ID Client] : " + aa
                         + "\t[Nom] : " + clientTmp.nomClient.ToString()
                         + "\t\t[Prenom] : " + clientTmp.prenomClient.ToString()
                         + "\t\t[Email] : " + clientTmp.emailClient.ToString()
                         + "\n\n");
                    }

                    fic.Close();
                }
            }
            else
            {
                using (StreamWriter fic = File.CreateText(nomRepertoire + "/" + nomFichier))
                {
                    fic.WriteLine("\t\t\t\t[LISTE DES CLIENTS INTERESSES]");
                    fic.WriteLine("\t\t\t\t---------------\n");

                    fic.WriteLine(" ______________________________________________________________________________");
                    fic.WriteLine("|                                                                              |");
                    fic.WriteLine("|                            INFORMATIONS DU VEHICULE                          |");
                    fic.WriteLine("|______________________________________________________________________________|\n");

                    fic.WriteLine("\n[ID Voiture] : " + tbIdVoiture.Text
                        + "\t[Nom Marque] : " + marqueTmp.nomMarque.ToString()
                        + "\t\t[Nom Modèle] : " + modeleTmp.nomModele.ToString()
                        + "\t\t[Nom Categorie] : " + catTmp.nomCat.ToString()
                        + "\t\t[Annee de fabrication] : " + voitTmp.anneeFabrication.ToString()
                        + "\t\t[Carburant] : " + carbuTmp.nomCarburant.ToString()
                        + "\t\t[Couleur] : " + coulTmp.nomCouleur.ToString()
                        + "\t\t[Kilométrage] : " + voitTmp.kilometrage.ToString()
                        + "\n");

                    fic.WriteLine(" ______________________________________________________________________________");
                    fic.WriteLine("|                                                                              |");
                    fic.WriteLine("|                            LISTE DES CLIENTS                                 |");
                    fic.WriteLine("|______________________________________________________________________________|\n");

                    DesideratasDataContext DCDesideratas = new DesideratasDataContext();
                    var requete = from desideratas in DCDesideratas.DesideratasVoiture
                                  where desideratas.idMarque == voitTmp.idMarque && desideratas.idModele == voitTmp.idModele
                                  select desideratas.idClient;

                    foreach (var aa in requete)
                    {
                        C_ClientsVoiture clientTmp = new G_ClientsVoiture(chConnexion).Lire_ID(aa);
                        fic.WriteLine("\n[ID Client] : " + aa
                         + "\t[Nom] : " + clientTmp.nomClient.ToString()
                         + "\t\t[Prenom] : " + clientTmp.prenomClient.ToString()
                         + "\t\t[Email] : " + clientTmp.emailClient.ToString()
                         + "\n\n");
                    }

                    fic.Close();
                }
            }
        }
        #endregion
    }
}
