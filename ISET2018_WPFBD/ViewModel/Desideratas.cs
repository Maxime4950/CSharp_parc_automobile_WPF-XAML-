using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Desideratas : BasePropriete
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
        private C_Desideratas _DesiderataSelectionne;
        public C_Desideratas DesiderataSelectionne
        {
            get { return _DesiderataSelectionne; }
            set { AssignerChamp<C_Desideratas>(ref _DesiderataSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnDesiderata _UnDesiderata;
        public VM_UnDesiderata UnDesiderata
        {
            get { return _UnDesiderata; }
            set { AssignerChamp<VM_UnDesiderata>(ref _UnDesiderata, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_Desideratas> _BcpDesideratas = new ObservableCollection<C_Desideratas>();
        public ObservableCollection<C_Desideratas> BcpDesideratas
        {
            get { return _BcpDesideratas; }
            set { _BcpDesideratas = value; }
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
        public VM_Desideratas()
        {
            UnDesiderata = new VM_UnDesiderata();
            UnDesiderata.IDDesiderata = 24;
            string Index = UnDesiderata.IDDesiderata.ToString();
            BcpDesideratas = ChargerDesideratas(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_Desideratas> ChargerDesideratas(string chConn, string Index)
        {
            ObservableCollection<C_Desideratas> rep = new ObservableCollection<C_Desideratas>();
            List<C_Desideratas> lTmp = new Model.G_Desideratas(chConn).Lire(Index);
            foreach (C_Desideratas Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnDesiderata.IDDesiderata = new Model.G_Desideratas(chConnexion).Ajouter(UnDesiderata.IDClient, UnDesiderata.IDMarque, UnDesiderata.IDModele, UnDesiderata.IDCat, UnDesiderata.Kilometrage, UnDesiderata.IDCouleur, UnDesiderata.IDCarburant, UnDesiderata.AnneeMin);
                BcpDesideratas.Add(new C_Desideratas(UnDesiderata.IDDesiderata,UnDesiderata.IDClient, UnDesiderata.IDMarque, UnDesiderata.IDModele, UnDesiderata.IDCat, UnDesiderata.Kilometrage, UnDesiderata.IDCouleur, UnDesiderata.IDCarburant, UnDesiderata.AnneeMin));
            }
            else
            {
                new Model.G_Desideratas(chConnexion).Modifier(UnDesiderata.IDDesiderata, UnDesiderata.IDClient, UnDesiderata.IDMarque, UnDesiderata.IDModele, UnDesiderata.IDCat, UnDesiderata.Kilometrage, UnDesiderata.IDCouleur, UnDesiderata.IDCarburant, UnDesiderata.AnneeMin);
                BcpDesideratas[nAjout] = new C_Desideratas(UnDesiderata.IDDesiderata, UnDesiderata.IDClient, UnDesiderata.IDMarque, UnDesiderata.IDModele, UnDesiderata.IDCat, UnDesiderata.Kilometrage, UnDesiderata.IDCouleur, UnDesiderata.IDCarburant, UnDesiderata.AnneeMin);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnDesiderata = new VM_UnDesiderata();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (DesiderataSelectionne != null)
            {
                C_Desideratas Tmp = new Model.G_Desideratas(chConnexion).Lire_ID(DesiderataSelectionne.idDesiterata);
                UnDesiderata = new VM_UnDesiderata();
                UnDesiderata.IDDesiderata = Tmp.idDesiterata;
                UnDesiderata.IDClient = Tmp.idClient;
                UnDesiderata.IDMarque = Tmp.idMarque;
                UnDesiderata.IDModele = Tmp.idModele;
                UnDesiderata.IDCat = Tmp.idCat;
                UnDesiderata.Kilometrage = Tmp.kilometrageMax;
                UnDesiderata.IDCouleur = Tmp.idCouleur;
                UnDesiderata.IDCarburant = Tmp.idCarburant;
                UnDesiderata.AnneeMin = Tmp.anneeMin;
                nAjout = BcpDesideratas.IndexOf(DesiderataSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (DesiderataSelectionne != null)
            {
                new Model.G_Desideratas(chConnexion).Supprimer(DesiderataSelectionne.idClient);
                BcpDesideratas.Remove(DesiderataSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_Desideratas p in lTmp)
            { string s = p.idDesiterata.ToString() ; } //a verifier
            int nTmp = lTmp.Count;
        }
        public void DesiderataSelectionne2UnDesiderata()
        {
            UnDesiderata.IDDesiderata = DesiderataSelectionne.idDesiterata;
            UnDesiderata.IDClient = DesiderataSelectionne.idClient;
            UnDesiderata.IDMarque = DesiderataSelectionne.idMarque;
            UnDesiderata.IDModele = DesiderataSelectionne.idModele;
            UnDesiderata.IDCat = DesiderataSelectionne.idCat;
            UnDesiderata.Kilometrage = DesiderataSelectionne.kilometrageMax;
            UnDesiderata.IDCouleur = DesiderataSelectionne.idCouleur;
            UnDesiderata.IDCarburant = DesiderataSelectionne.idCarburant;
            UnDesiderata.AnneeMin = DesiderataSelectionne.anneeMin;
        }
    }
    public class VM_UnDesiderata : BasePropriete
    {
        private int _IDDesiderata, _IDClient, _IDMarque;
        private int? _IDModele, _IDCat, _Kilometrage,_IDCouleur,_IDCarburant,_AnneeMin;
        public int IDDesiderata
        {
            get { return _IDDesiderata; }
            set { AssignerChamp<int>(ref _IDDesiderata, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDClient
        {
            get { return _IDClient; }
            set { AssignerChamp<int>(ref _IDClient, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDMarque
        {
            get { return _IDMarque; }
            set { AssignerChamp<int>(ref _IDMarque, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IDModele
        {
            get { return _IDModele; }
            set { AssignerChamp<int?>(ref _IDModele, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IDCat
        {
            get { return _IDCat; }
            set { AssignerChamp<int?>(ref _IDCat, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? Kilometrage
        {
            get { return _Kilometrage; }
            set { AssignerChamp<int?>(ref _Kilometrage, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IDCouleur
        {
            get { return _IDCouleur; }
            set { AssignerChamp<int?>(ref _IDCouleur, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IDCarburant
        {
            get { return _IDCarburant; }
            set { AssignerChamp<int?>(ref _IDCarburant, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? AnneeMin
        {
            get { return _AnneeMin; }
            set { AssignerChamp<int?>(ref _AnneeMin, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}
