using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Couleur
    {
        #region Données membres
        private int _idCouleur;
        private string _nomCouleur;
        #endregion
        #region Constructeurs
        public C_Couleur()
        { }
        public C_Couleur(string nomCouleur_)
        {
            nomCouleur = nomCouleur_;
        }
        public C_Couleur(int idCouleur_, string nomCouleur_)
         : this(nomCouleur_)
        {
            idCouleur = idCouleur_;
        }
        #endregion
        #region Accesseurs
        public int idCouleur
        {
            get { return _idCouleur; }
            set { _idCouleur = value; }
        }
        public string nomCouleur
        {
            get { return _nomCouleur; }
            set { _nomCouleur = value; }
        }
        #endregion
    }
}
