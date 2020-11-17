using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class G_Base
    {
        #region Données membres
        string _ChaineConnexion;
        #endregion
        #region Constructeurs
        public G_Base()
        { ChaineConnexion = ""; }
        public G_Base(string sChaineConnexion)
        { ChaineConnexion = sChaineConnexion; }
        #endregion
        #region Accesseur
        public string ChaineConnexion
        {
            get { return _ChaineConnexion; }
            set { _ChaineConnexion = value; }
        }
        #endregion
    }
}
