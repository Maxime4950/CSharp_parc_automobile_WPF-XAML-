﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Desideratas
    {
        #region Données membres
        private int _idDesiderata;
        private int _idClient;
        private int _idMarque;
        private int? _idModele;
        private int? _idCat;
        private int? _kilometrageMax;
        private int? _idCouleur;
        private int? _idCarburant;
        private int? _anneeMin;
        #endregion
        #region Constructeurs
        public C_Desideratas()
        { }
        public C_Desideratas(int idClient_, int idMarque_, int? idModele_, int? idCat_, int? kilometrageMax_, int? idCouleur_, int? idCarburant_, int? anneeMin_)
        {
            idClient = idClient_;
            idMarque = idMarque_;
            idModele = idModele_;
            idCat = idCat_;
            kilometrageMax = kilometrageMax_;
            idCouleur = idCouleur_;
            idCarburant = idCarburant_;
            anneeMin = anneeMin_;
        }
        public C_Desideratas(int idDesiderata_, int idClient_, int idMarque_, int? idModele_, int? idCat_, int? kilometrageMax_, int? idCouleur_, int? idCarburant_, int? anneeMin_)
         : this(idClient_, idMarque_, idModele_, idCat_, kilometrageMax_, idCouleur_, idCarburant_, anneeMin_)
        {
            idDesiderata = idDesiderata_;
        }
        #endregion
        #region Accesseurs
        public int idDesiderata
        {
            get { return _idDesiderata; }
            set { _idDesiderata = value; }
        }
        public int idClient
        {
            get { return _idClient; }
            set { _idClient = value; }
        }
        public int idMarque
        {
            get { return _idMarque; }
            set { _idMarque = value; }
        }
        public int? idModele
        {
            get { return _idModele; }
            set { _idModele = value; }
        }
        public int? idCat
        {
            get { return _idCat; }
            set { _idCat = value; }
        }
        public int? kilometrageMax
        {
            get { return _kilometrageMax; }
            set { _kilometrageMax = value; }
        }
        public int? idCouleur
        {
            get { return _idCouleur; }
            set { _idCouleur = value; }
        }
        public int? idCarburant
        {
            get { return _idCarburant; }
            set { _idCarburant = value; }
        }
        public int? anneeMin
        {
            get { return _anneeMin; }
            set { _anneeMin = value; }
        }
        #endregion
    }
}
