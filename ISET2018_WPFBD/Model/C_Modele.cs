﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Modele
    {
        #region Données membres
        private int _idModele;
        private string _nomModele;
        #endregion
        #region Constructeurs
        public C_Modele()
        { }
        public C_Modele(string nomModele_)
        {
            nomModele = nomModele_;
        }
        public C_Modele(int idModele_, string nomModele_)
         : this(nomModele_)
        {
            idModele = idModele_;
        }
        #endregion
        #region Accesseurs
        public int idModele
        {
            get { return _idModele; }
            set { _idModele = value; }
        }
        public string nomModele
        {
            get { return _nomModele; }
            set { _nomModele = value; }
        }
        #endregion
    }
}
