using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Achat : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Maesm\Documents\Complement_P\ISET2018_WPFBD_MVVM_concept\ISET2018_WPFBD\BD_Voiture_mvvm.mdf;Integrated Security=True;Connect Timeout=30";
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
        private C_AchatVente _AchatSelectionnee;
        public C_AchatVente AchatSelectionnee
        {
            get { return _AchatSelectionnee; }
            set { AssignerChamp<C_AchatVente>(ref _AchatSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnAchat _UnAchat;
        public VM_UnAchat UnAchat
        {
            get { return _UnAchat; }
            set { AssignerChamp<VM_UnAchat>(ref _UnAchat, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_AchatVente> _BcpAchats = new ObservableCollection<C_AchatVente>();
        public ObservableCollection<C_AchatVente> BcpAchats
        {
            get { return _BcpAchats; }
            set { _BcpAchats = value; }
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
        public VM_Achat()
        {
            UnAchat = new VM_UnAchat();
            string Index = UnAchat.IDOperation.ToString();
            BcpAchats = ChargerAchat(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_AchatVente> ChargerAchat(string chConn, string Index)
        {
            ObservableCollection<C_AchatVente> rep = new ObservableCollection<C_AchatVente>();
            List<C_AchatVente> lTmp = new Model.G_AchatVente(chConn).Lire(Index);
            foreach (C_AchatVente Tmp in lTmp)
                if(Tmp.typeOperation == "achat")
                { rep.Add(Tmp); }
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnAchat.IDOperation = new Model.G_AchatVente(chConnexion).Ajouter(UnAchat.IDVoiture, UnAchat.IDClient, UnAchat.Prix, UnAchat.Date, UnAchat.IDPaiement, UnAchat.Type);
                BcpAchats.Add(new C_AchatVente(UnAchat.IDOperation, UnAchat.IDVoiture, UnAchat.IDClient, UnAchat.Prix, UnAchat.Date, UnAchat.IDPaiement, UnAchat.Type));
            }
            else
            {
                new Model.G_AchatVente(chConnexion).Modifier(UnAchat.IDOperation, UnAchat.IDVoiture, UnAchat.IDClient, UnAchat.Prix, UnAchat.Date, UnAchat.IDPaiement, UnAchat.Type);
                BcpAchats[nAjout] = new C_AchatVente(UnAchat.IDOperation, UnAchat.IDVoiture, UnAchat.IDClient, UnAchat.Prix, UnAchat.Date, UnAchat.IDPaiement, UnAchat.Type);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnAchat = new VM_UnAchat();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (AchatSelectionnee != null)
            {
                C_AchatVente Tmp = new Model.G_AchatVente(chConnexion).Lire_ID(AchatSelectionnee.idOperation);
                UnAchat = new VM_UnAchat();
                UnAchat.IDOperation = Tmp.idOperation;
                UnAchat.IDVoiture = Tmp.idVoiture;
                UnAchat.IDClient = Tmp.idClient;
                UnAchat.Prix = Tmp.prixOperation;
                UnAchat.Date = Tmp.dateOperation;
                UnAchat.IDPaiement = Tmp.idPaiement;
                UnAchat.Type = Tmp.typeOperation;
                nAjout = BcpAchats.IndexOf(AchatSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (AchatSelectionnee != null)
            {
                new Model.G_ClientsVoiture(chConnexion).Supprimer(AchatSelectionnee.idOperation);
                BcpAchats.Remove(AchatSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_ClientsVoiture p in lTmp)
            { string s = p.nomClient; }
            int nTmp = lTmp.Count;
        }
        public void AchatSelectionnee2UnAchat()
        {
            UnAchat.IDOperation = AchatSelectionnee.idOperation;
            UnAchat.IDVoiture = AchatSelectionnee.idVoiture;
            UnAchat.IDClient = AchatSelectionnee.idClient;
            UnAchat.Prix = AchatSelectionnee.prixOperation;
            UnAchat.Date = AchatSelectionnee.dateOperation;
            UnAchat.IDPaiement = AchatSelectionnee.idPaiement;
            UnAchat.Type = AchatSelectionnee.typeOperation;
        }
    }
    public class VM_UnAchat : BasePropriete
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
