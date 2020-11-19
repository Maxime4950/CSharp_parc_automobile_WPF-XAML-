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
    public class G_AchatVente : G_Base
    {
        #region Constructeurs
        public G_AchatVente()
         : base()
        { }
        public G_AchatVente(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(int idVoiture, int idClient, int prixOperation, DateTime dateOperation, int idPaiement, string typeOperation)
        { return new A_AchatVente(ChaineConnexion).Ajouter(idVoiture, idClient, prixOperation, dateOperation, idPaiement, typeOperation); }
        public int Modifier(int idOperation, int idVoiture, int idClient, int prixOperation, DateTime dateOperation, int idPaiement, string typeOperation)
        { return new A_AchatVente(ChaineConnexion).Modifier(idOperation, idVoiture, idClient, prixOperation, dateOperation, idPaiement, typeOperation); }
        public List<C_AchatVente> Lire(string Index)
        { return new A_AchatVente(ChaineConnexion).Lire(Index); }
        public C_AchatVente Lire_ID(int idOperation)
        { return new A_AchatVente(ChaineConnexion).Lire_ID(idOperation); }
        public int Supprimer(int idOperation)
        { return new A_AchatVente(ChaineConnexion).Supprimer(idOperation); }
    }
}
