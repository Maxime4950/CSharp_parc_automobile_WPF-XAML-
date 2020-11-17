#region Ressources extérieures
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace ISET2018_WPFBD.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_StockVoiture : G_Base
    {
        #region Constructeurs
        public G_StockVoiture()
         : base()
        { }
        public G_StockVoiture(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(int idMarque, int idModele, int idCategorie, int? anneeFabrication, int? idCarburant, int? idCouleur, int? kilometrage)
        { return new A_StockVoiture(ChaineConnexion).Ajouter(idMarque, idModele, idCategorie, anneeFabrication, idCarburant, idCouleur, kilometrage); }
        public int Modifier(int idVoiture, int idMarque, int idModele, int idCategorie, int? anneeFabrication, int? idCarburant, int? idCouleur, int? kilometrage)
        { return new A_StockVoiture(ChaineConnexion).Modifier(idVoiture, idMarque, idModele, idCategorie, anneeFabrication, idCarburant, idCouleur, kilometrage); }
        public List<C_StockVoiture> Lire(string Index)
        { return new A_StockVoiture(ChaineConnexion).Lire(Index); }
        public C_StockVoiture Lire_ID(int idVoiture)
        { return new A_StockVoiture(ChaineConnexion).Lire_ID(idVoiture); }
        public int Supprimer(int idVoiture)
        { return new A_StockVoiture(ChaineConnexion).Supprimer(idVoiture); }
    }
}
