using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Desideratas : G_Base
    {
        #region Constructeurs
        public G_Desideratas()
         : base()
        { }
        public G_Desideratas(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(int idClient, int idMarque, int? idModele, int? idCat, int? kilometrageMax, int? idCouleur, int? idCarburant, int? anneeMin)
        { return new A_Desideratas(ChaineConnexion).Ajouter(idClient, idMarque, idModele, idCat, kilometrageMax, idCouleur, idCarburant, anneeMin); }
        public int Modifier(int idDesiterata, int idClient, int idMarque, int? idModele, int? idCat, int? kilometrageMax, int? idCouleur, int? idCarburant, int? anneeMin)
        { return new A_Desideratas(ChaineConnexion).Modifier(idDesiterata, idClient, idMarque, idModele, idCat, kilometrageMax, idCouleur, idCarburant, anneeMin); }
        public List<C_Desideratas> Lire(string Index)
        { return new A_Desideratas(ChaineConnexion).Lire(Index); }
        public C_Desideratas Lire_ID(int idDesiterata)
        { return new A_Desideratas(ChaineConnexion).Lire_ID(idDesiterata); }
        public int Supprimer(int idDesiterata)
        { return new A_Desideratas(ChaineConnexion).Supprimer(idDesiterata); }
    }
}
