#region Ressources extérieures
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace ISET2018_WPFBD.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_Paiement : G_Base
    {
        #region Constructeurs
        public G_Paiement()
         : base()
        { }
        public G_Paiement(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomPaiement)
        { return new A_Paiement(ChaineConnexion).Ajouter(nomPaiement); }
        public int Modifier(int idPaiement, string nomPaiement)
        { return new A_Paiement(ChaineConnexion).Modifier(idPaiement, nomPaiement); }
        public List<C_Paiement> Lire(string Index)
        { return new A_Paiement(ChaineConnexion).Lire(Index); }
        public C_Paiement Lire_ID(int idPaiement)
        { return new A_Paiement(ChaineConnexion).Lire_ID(idPaiement); }
        public int Supprimer(int idPaiement)
        { return new A_Paiement(ChaineConnexion).Supprimer(idPaiement); }
    }
}
