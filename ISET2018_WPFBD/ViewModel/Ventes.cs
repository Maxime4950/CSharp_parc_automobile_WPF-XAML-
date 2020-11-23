using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Vente : BasePropriete
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
        private C_AchatVente _VenteSelectionnee;
        public C_AchatVente VenteSelectionnee
        {
            get { return _VenteSelectionnee; }
            set { AssignerChamp<C_AchatVente>(ref _VenteSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UneVente _UneVente;
        public VM_UneVente UneVente
        {
            get { return _UneVente; }
            set { AssignerChamp<VM_UneVente>(ref _UneVente, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_AchatVente> _BcpVente = new ObservableCollection<C_AchatVente>();
        public ObservableCollection<C_AchatVente> BcpVente
        {
            get { return _BcpVente; }
            set { _BcpVente = value; }
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
        public VM_Vente()
        {
            UneVente = new VM_UneVente();
            string Index = UneVente.IDOperation.ToString();
            BcpVente = ChargerVente(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_AchatVente> ChargerVente(string chConn, string Index)
        {
            ObservableCollection<C_AchatVente> rep = new ObservableCollection<C_AchatVente>();
            List<C_AchatVente> lTmp = new Model.G_AchatVente(chConn).Lire(Index);
            foreach (C_AchatVente Tmp in lTmp)
                if(Tmp.typeOperation == "vente")
                { rep.Add(Tmp); }
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneVente.IDOperation = new Model.G_AchatVente(chConnexion).Ajouter(UneVente.IDVoiture, UneVente.IDClient, UneVente.Prix, UneVente.Date, UneVente.IDPaiement, UneVente.Type);
                BcpVente.Add(new C_AchatVente(UneVente.IDOperation, UneVente.IDVoiture, UneVente.IDClient, UneVente.Prix, UneVente.Date, UneVente.IDPaiement, UneVente.Type));
            }
            else
            {
                new Model.G_AchatVente(chConnexion).Modifier(UneVente.IDOperation, UneVente.IDVoiture, UneVente.IDClient, UneVente.Prix, UneVente.Date, UneVente.IDPaiement, UneVente.Type);
                BcpVente[nAjout] = new C_AchatVente(UneVente.IDOperation, UneVente.IDVoiture, UneVente.IDClient, UneVente.Prix, UneVente.Date, UneVente.IDPaiement, UneVente.Type);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UneVente = new VM_UneVente();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (VenteSelectionnee != null)
            {
                C_AchatVente Tmp = new Model.G_AchatVente(chConnexion).Lire_ID(VenteSelectionnee.idOperation);
                UneVente = new VM_UneVente();
                UneVente.IDOperation = Tmp.idOperation;
                UneVente.IDVoiture = Tmp.idVoiture;
                UneVente.IDClient = Tmp.idClient;
                UneVente.Prix = Tmp.prixOperation;
                UneVente.Date = Tmp.dateOperation;
                UneVente.IDPaiement = Tmp.idPaiement;
                UneVente.Type = Tmp.typeOperation;
                nAjout = BcpVente.IndexOf(VenteSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (VenteSelectionnee != null)
            {
                new Model.G_ClientsVoiture(chConnexion).Supprimer(VenteSelectionnee.idOperation);
                BcpVente.Remove(VenteSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_ClientsVoiture p in lTmp)
            { string s = p.nomClient; }
            int nTmp = lTmp.Count;
        }
        public void VenteSelectionnee2UneVente()
        {
            UneVente.IDOperation = VenteSelectionnee.idOperation;
            UneVente.IDVoiture = VenteSelectionnee.idVoiture;
            UneVente.IDClient = VenteSelectionnee.idClient;
            UneVente.Prix = VenteSelectionnee.prixOperation;
            UneVente.Date = VenteSelectionnee.dateOperation;
            UneVente.IDPaiement = VenteSelectionnee.idPaiement;
            UneVente.Type = VenteSelectionnee.typeOperation;
        }
    }
    public class VM_UneVente : BasePropriete
    {
        private int _IDOperation, _IDVoiture, _IDClient, _Prix, _IDPaiement;
        private DateTime _Date;
        private string _Type;
        public int IDOperation
        {
            get { return _IDOperation; }
            set { AssignerChamp<int>(ref _IDOperation, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDVoiture
        {
            get { return _IDVoiture; }
            set { AssignerChamp<int>(ref _IDVoiture, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDClient
        {
            get { return _IDClient; }
            set { AssignerChamp<int>(ref _IDClient, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int Prix
        {
            get { return _Prix; }
            set { AssignerChamp<int>(ref _Prix, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDPaiement
        {
            get { return _IDPaiement; }
            set { AssignerChamp<int>(ref _IDPaiement, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime Date 
        {
            get { return _Date; }
            set { AssignerChamp<DateTime>(ref _Date, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Type
        {
            get { return _Type; }
            set { AssignerChamp<string>(ref _Type, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}
