using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Couleur : G_Base
    {
        #region Constructeurs
        public G_Couleur()
         : base()
        { }
        public G_Couleur(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomCouleur)
        { return new A_Couleur(ChaineConnexion).Ajouter(nomCouleur); }
        public int Modifier(int idCouleur, string nomCouleur)
        { return new A_Couleur(ChaineConnexion).Modifier(idCouleur, nomCouleur); }
        public List<C_Couleur> Lire(string Index)
        { return new A_Couleur(ChaineConnexion).Lire(Index); }
        public C_Couleur Lire_ID(int idCouleur)
        { return new A_Couleur(ChaineConnexion).Lire_ID(idCouleur); }
        public int Supprimer(int idCouleur)
        { return new A_Couleur(ChaineConnexion).Supprimer(idCouleur); }
    }
}
