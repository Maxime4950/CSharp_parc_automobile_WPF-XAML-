using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Marque : G_Base
    {
        #region Constructeurs
        public G_Marque()
         : base()
        { }
        public G_Marque(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomMarque, string paysMarque)
        { return new A_Marque(ChaineConnexion).Ajouter(nomMarque, paysMarque); }
        public int Modifier(int idMarque, string nomMarque, string paysMarque)
        { return new A_Marque(ChaineConnexion).Modifier(idMarque, nomMarque, paysMarque); }
        public List<C_Marque> Lire(string Index)
        { return new A_Marque(ChaineConnexion).Lire(Index); }
        public C_Marque Lire_ID(int idMarque)
        { return new A_Marque(ChaineConnexion).Lire_ID(idMarque); }
        public int Supprimer(int idMarque)
        { return new A_Marque(ChaineConnexion).Supprimer(idMarque); }
    }
}
