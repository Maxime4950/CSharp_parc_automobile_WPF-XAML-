using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Carburant : G_Base
    {
        #region Constructeurs
        public G_Carburant()
         : base()
        { }
        public G_Carburant(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomCarburant)
        { return new A_Carburant(ChaineConnexion).Ajouter(nomCarburant); }
        public int Modifier(int idCarburant, string nomCarburant)
        { return new A_Carburant(ChaineConnexion).Modifier(idCarburant, nomCarburant); }
        public List<C_Carburant> Lire(string Index)
        { return new A_Carburant(ChaineConnexion).Lire(Index); }
        public C_Carburant Lire_ID(int idCarburant)
        { return new A_Carburant(ChaineConnexion).Lire_ID(idCarburant); }
        public int Supprimer(int idCarburant)
        { return new A_Carburant(ChaineConnexion).Supprimer(idCarburant); }
    }
}
