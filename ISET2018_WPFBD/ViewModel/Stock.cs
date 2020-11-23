using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Stock : BasePropriete
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
        private C_StockVoiture _StockSelectionnee;
        public C_StockVoiture StockSelectionnee
        {
            get { return _StockSelectionnee; }
            set { AssignerChamp<C_StockVoiture>(ref _StockSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnStock _UnStock;
        public VM_UnStock UnStock
        {
            get { return _UnStock; }
            set { AssignerChamp<VM_UnStock>(ref _UnStock, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_StockVoiture> _BcpStock = new ObservableCollection<C_StockVoiture>();
        public ObservableCollection<C_StockVoiture> BcpStock
        {
            get { return _BcpStock; }
            set { _BcpStock = value; }
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
        public VM_Stock()
        {
            UnStock = new VM_UnStock();
            UnStock.IdVoiture = 12;
            string Index = UnStock.IdVoiture.ToString();
            BcpStock = ChargerStock(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_StockVoiture> ChargerStock(string chConn, string Index)
        {
            ObservableCollection<C_StockVoiture> rep = new ObservableCollection<C_StockVoiture>();
            List<C_StockVoiture> lTmp = new Model.G_StockVoiture(chConn).Lire(Index);
            foreach (C_StockVoiture Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnStock.IdVoiture = new Model.G_StockVoiture(chConnexion).Ajouter(UnStock.IdMarque, UnStock.IdModele,UnStock.IdCategorie,UnStock.AnneeFabr,UnStock.IdCarburant,UnStock.IdCouleur,UnStock.Kilometrage);
                BcpStock.Add(new C_StockVoiture(UnStock.IdMarque, UnStock.IdModele, UnStock.IdCategorie, UnStock.AnneeFabr, UnStock.IdCarburant, UnStock.IdCouleur, UnStock.Kilometrage));
            }
            else
            {
                new Model.G_StockVoiture(chConnexion).Modifier(UnStock.IdVoiture,UnStock.IdMarque, UnStock.IdModele, UnStock.IdCategorie, UnStock.AnneeFabr, UnStock.IdCarburant, UnStock.IdCouleur, UnStock.Kilometrage);
                BcpStock[nAjout] = new C_StockVoiture(UnStock.IdVoiture,UnStock.IdMarque, UnStock.IdModele, UnStock.IdCategorie, UnStock.AnneeFabr, UnStock.IdCarburant, UnStock.IdCouleur, UnStock.Kilometrage);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnStock = new VM_UnStock();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (StockSelectionnee != null)
            {
                C_StockVoiture Tmp = new Model.G_StockVoiture(chConnexion).Lire_ID(StockSelectionnee.idVoiture);
                UnStock = new VM_UnStock();
                UnStock.IdVoiture = Tmp.idVoiture;
                UnStock.IdMarque = Tmp.idMarque;
                UnStock.IdModele = Tmp.idModele;
                UnStock.IdCategorie = Tmp.idCategorie;
                UnStock.AnneeFabr = Tmp.anneeFabrication ;
                UnStock.IdCarburant = Tmp.idCarburant;
                UnStock.IdCouleur = Tmp.idCouleur;
                UnStock.Kilometrage = Tmp.kilometrage;
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (StockSelectionnee != null)
            {
                new Model.G_StockVoiture(chConnexion).Supprimer(StockSelectionnee.idVoiture);
                BcpStock.Remove(StockSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_ClientsVoiture p in lTmp)
            { string s = p.nomClient; }
            int nTmp = lTmp.Count;
        }
        public void StockSelectionnee2UnStock()
        {
            UnStock.IdVoiture = StockSelectionnee.idVoiture;
            UnStock.IdMarque = StockSelectionnee.idMarque;
            UnStock.IdModele = StockSelectionnee.idModele;
            UnStock.IdCategorie = StockSelectionnee.idCategorie;
            UnStock.AnneeFabr = StockSelectionnee.anneeFabrication;
            UnStock.IdCarburant = StockSelectionnee.idCarburant;
            UnStock.IdCouleur = StockSelectionnee.idCouleur;
            UnStock.Kilometrage = StockSelectionnee.kilometrage;
        }
    }
    public class VM_UnStock : BasePropriete
    {
        private int _IDVoiture, _IDMarque, _IDModele, _IDCategorie;
        private int? _AnneeFabr, _IDCarburant, _IDCouleur, _Kilometrage;
        public int IdVoiture
        {
            get { return _IDVoiture; }
            set { AssignerChamp<int>(ref _IDVoiture, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IdMarque
        {
            get { return _IDMarque; }
            set { AssignerChamp<int>(ref _IDMarque, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IdModele
        {
            get { return _IDModele; }
            set { AssignerChamp<int>(ref _IDModele, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IdCategorie
        {
            get { return _IDCategorie; }
            set { AssignerChamp<int>(ref _IDCategorie, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? AnneeFabr
        {
            get { return _AnneeFabr; }
            set { AssignerChamp<int?>(ref _AnneeFabr, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IdCarburant
        {
            get { return _IDCarburant; }
            set { AssignerChamp<int?>(ref _IDCarburant, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IdCouleur
        {
            get { return _IDCouleur; }
            set { AssignerChamp<int?>(ref _IDCouleur, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? Kilometrage
        {
            get { return _Kilometrage; }
            set { AssignerChamp<int?>(ref _Kilometrage, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }


    }
}
