using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Carburant
    {
        #region Données membres
        private int _idCarburant;
        private string _nomCarburant;
        #endregion
        #region Constructeurs
        public C_Carburant()
        { }
        public C_Carburant(string nomCarburant_)
        {
            nomCarburant = nomCarburant_;
        }
        public C_Carburant(int idCarburant_, string nomCarburant_)
         : this(nomCarburant_)
        {
            idCarburant = idCarburant_;
        }
        #endregion
        #region Accesseurs
        public int idCarburant
        {
            get { return _idCarburant; }
            set { _idCarburant = value; }
        }
        public string nomCarburant
        {
            get { return _nomCarburant; }
            set { _nomCarburant = value; }
        }
        #endregion
    }
}
