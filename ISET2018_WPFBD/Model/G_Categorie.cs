using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Categorie : G_Base
    {
        #region Constructeurs
        public G_Categorie()
         : base()
        { }
        public G_Categorie(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomCat)
        { return new A_Categorie(ChaineConnexion).Ajouter(nomCat); }
        public int Modifier(int idCat, string nomCat)
        { return new A_Categorie(ChaineConnexion).Modifier(idCat, nomCat); }
        public List<C_Categorie> Lire(string Index)
        { return new A_Categorie(ChaineConnexion).Lire(Index); }
        public C_Categorie Lire_ID(int idCat)
        { return new A_Categorie(ChaineConnexion).Lire_ID(idCat); }
        public int Supprimer(int idCat)
        { return new A_Categorie(ChaineConnexion).Supprimer(idCat); }
    }
}
