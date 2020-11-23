using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISET2018_WPFBD.Model
{
    public class C_Frais
    {
        #region Données membres
        private int _idFrais;
        private int _idVoiture;
        private string _nomFrais;
        private string _descriptionFrais;
        private int _coutFrais;
        #endregion
        #region Constructeurs
        public C_Frais()
        { }
        public C_Frais(int idVoiture_, string nomFrais_, string descriptionFrais_, int coutFrais_)
        {
            idVoiture = idVoiture_;
            nomFrais = nomFrais_;
            descriptionFrais = descriptionFrais_;
            coutFrais = coutFrais_;
        }
        public C_Frais(int idFrais_, int idVoiture_, string nomFrais_, string descriptionFrais_, int coutFrais_)
         : this(idVoiture_, nomFrais_, descriptionFrais_, coutFrais_)
        {
            idFrais = idFrais_;
        }
        #endregion
        #region Accesseurs
        public int idFrais
        {
            get { return _idFrais; }
            set { _idFrais = value; }
        }
        public int idVoiture
        {
            get { return _idVoiture; }
            set { _idVoiture = value; }
        }
        public string nomFrais
        {
            get { return _nomFrais; }
            set { _nomFrais = value; }
        }
        public string descriptionFrais
        {
            get { return _descriptionFrais; }
            set { _descriptionFrais = value; }
        }
        public int coutFrais
        {
            get { return _coutFrais; }
            set { _coutFrais = value; }
        }
        #endregion
    }
}