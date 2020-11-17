using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Marque
    {
        #region Données membres
        private int _idMarque;
        private string _nomMarque;
        private string _paysMarque;
        #endregion
        #region Constructeurs
        public C_Marque()
        { }
        public C_Marque(string nomMarque_, string paysMarque_)
        {
            nomMarque = nomMarque_;
            paysMarque = paysMarque_;
        }
        public C_Marque(int idMarque_, string nomMarque_, string paysMarque_)
         : this(nomMarque_, paysMarque_)
        {
            idMarque = idMarque_;
        }
        #endregion
        #region Accesseurs
        public int idMarque
        {
            get { return _idMarque; }
            set { _idMarque = value; }
        }
        public string nomMarque
        {
            get { return _nomMarque; }
            set { _nomMarque = value; }
        }
        public string paysMarque
        {
            get { return _paysMarque; }
            set { _paysMarque = value; }
        }
        #endregion
    }
}
