using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Categorie
    {
        #region Données membres
        private int _idCat;
        private string _nomCat;
        #endregion
        #region Constructeurs
        public C_Categorie()
        { }
        public C_Categorie(string nomCat_)
        {
            nomCat = nomCat_;
        }
        public C_Categorie(int idCat_, string nomCat_)
         : this(nomCat_)
        {
            idCat = idCat_;
        }
        #endregion
        #region Accesseurs
        public int idCat
        {
            get { return _idCat; }
            set { _idCat = value; }
        }
        public string nomCat
        {
            get { return _nomCat; }
            set { _nomCat = value; }
        }
        #endregion
    }
}
