using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Frais : G_Base
    {
        #region Constructeurs
        public G_Frais()
         : base()
        { }
        public G_Frais(string sChaineConnexion)
         : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(int idVoiture, string nomFrais, string descriptionFrais, int coutFrais)
        { return new A_Frais(ChaineConnexion).Ajouter(idVoiture, nomFrais,descriptionFrais,coutFrais); }
        public int Modifier(int idFrais, int idVoiture, string nomFrais, string descriptionFrais, int coutFrais)
        { return new A_Frais(ChaineConnexion).Modifier(idFrais, idVoiture, nomFrais, descriptionFrais, coutFrais); }
        public List<C_Frais> Lire(string Index)
        { return new A_Frais(ChaineConnexion).Lire(Index); }
        public C_Frais Lire_ID(int idFrais)
        { return new A_Frais(ChaineConnexion).Lire_ID(idFrais); }
        public int Supprimer(int idFrais)
        { return new A_Frais(ChaineConnexion).Supprimer(idFrais); }
    }
}
