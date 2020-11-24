using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Frais : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        private int nAjout;
        private bool _ActiverUneFiche;
        public bool ActiverUneFiche
        {
            get { return _ActiverUneFiche; }
            set
            {
                AssignerChamp<bool>(ref _ActiverUneFiche, value, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ActiverBcpFiche = !ActiverUneFiche;
            }
        }
        private bool _ActiverBcpFiche;
        public bool ActiverBcpFiche
        {
            get { return _ActiverBcpFiche; }
            set { AssignerChamp<bool>(ref _ActiverBcpFiche, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private C_Frais _FraisSelectionne;
        public C_Frais FraisSelectionne
        {
            get { return _FraisSelectionne; }
            set { AssignerChamp<C_Frais>(ref _FraisSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnFrais _UnFrais;
        public VM_UnFrais UnFrais
        {
            get { return _UnFrais; }
            set { AssignerChamp<VM_UnFrais>(ref _UnFrais, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_Frais> _BcpFrais = new ObservableCollection<C_Frais>();
        public ObservableCollection<C_Frais> BcpFrais
        {
            get { return _BcpFrais; }
            set { _BcpFrais = value; }
        }
        #endregion

        #region Commandes
        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }
        public BaseCommande cEssaiSelMult { get; set; }
        #endregion
        public VM_Frais()
        {
            UnFrais = new VM_UnFrais();
            UnFrais.IDFrais= 24;
            string Index = UnFrais.IDFrais.ToString();
            BcpFrais = ChargerFrais(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_Frais> ChargerFrais(string chConn, string Index)
        {
            ObservableCollection<C_Frais> rep = new ObservableCollection<C_Frais>();
            List<C_Frais> lTmp = new Model.G_Frais(chConn).Lire(Index);
            foreach (C_Frais Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnFrais.IDFrais = new Model.G_Frais(chConnexion).Ajouter(UnFrais.IDVoiture, UnFrais.NomFrais, UnFrais.DescriptionFrais, UnFrais.CoutFrais) ;
                BcpFrais.Add(new C_Frais(UnFrais.IDFrais, UnFrais.IDVoiture, UnFrais.NomFrais, UnFrais.DescriptionFrais, UnFrais.CoutFrais));
            }
            else
            {
                new Model.G_Frais(chConnexion).Modifier(UnFrais.IDFrais,UnFrais.IDVoiture, UnFrais.NomFrais, UnFrais.DescriptionFrais, UnFrais.CoutFrais);
                BcpFrais[nAjout] = new C_Frais(UnFrais.IDFrais, UnFrais.IDVoiture, UnFrais.NomFrais, UnFrais.DescriptionFrais, UnFrais.CoutFrais);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnFrais = new VM_UnFrais();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (FraisSelectionne != null)
            {
                C_Frais Tmp = new Model.G_Frais(chConnexion).Lire_ID(FraisSelectionne.idFrais);
                UnFrais = new VM_UnFrais();
                UnFrais.IDFrais = Tmp.idFrais;
                UnFrais.IDVoiture = Tmp.idVoiture;
                UnFrais.NomFrais = Tmp.nomFrais;
                UnFrais.DescriptionFrais = Tmp.descriptionFrais;
                UnFrais.CoutFrais = Tmp.coutFrais;
                nAjout = BcpFrais.IndexOf(FraisSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (FraisSelectionne != null)
            {
                new Model.G_Frais(chConnexion).Supprimer(FraisSelectionne.idFrais);
                BcpFrais.Remove(FraisSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_Frais p in lTmp)
            { string s = p.nomFrais; }
            int nTmp = lTmp.Count;
        }
        public void FraisSelectionnee2UnFrais()
        {
            UnFrais.IDFrais = FraisSelectionne.idFrais;
            UnFrais.IDVoiture = FraisSelectionne.idVoiture;
            UnFrais.NomFrais= FraisSelectionne.nomFrais;
            UnFrais.DescriptionFrais = FraisSelectionne.descriptionFrais;
            UnFrais.CoutFrais = FraisSelectionne.coutFrais;
        }
    }
    public class VM_UnFrais : BasePropriete
    {
        private int _IDFrais, _IDVoiture, _CoutFrais;
        private string _NomFrais, _DescriptionFrais;
        public int IDFrais
        {
            get { return _IDFrais; }
            set { AssignerChamp<int>(ref _IDFrais, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDVoiture
        {
            get { return _IDVoiture; }
            set { AssignerChamp<int>(ref _IDVoiture, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string NomFrais
        {
            get { return _NomFrais; }
            set { AssignerChamp<string>(ref _NomFrais, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string DescriptionFrais
        {
            get { return _DescriptionFrais; }
            set { AssignerChamp<string>(ref _DescriptionFrais, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int CoutFrais
        {
            get { return _CoutFrais; }
            set { AssignerChamp<int>(ref _CoutFrais, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}
