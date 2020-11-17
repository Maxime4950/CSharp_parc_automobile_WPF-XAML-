using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Modele : G_Base
    {
        #region Constructeurs
        public G_Modele()
         : base()
        { }
        public G_Modele(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomModele)
        { return new A_Modele(ChaineConnexion).Ajouter(nomModele); }
        public int Modifier(int idModele, string nomModele)
        { return new A_Modele(ChaineConnexion).Modifier(idModele, nomModele); }
        public List<C_Modele> Lire(string Index)
        { return new A_Modele(ChaineConnexion).Lire(Index); }
        public C_Modele Lire_ID(int idModele)
        { return new A_Modele(ChaineConnexion).Lire_ID(idModele); }
        public int Supprimer(int idModele)
        { return new A_Modele(ChaineConnexion).Supprimer(idModele); }
    }
}
