using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_AchatVente : BasePropriete
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
        private C_AchatVente _AchatVenteSelectionne;
        public C_AchatVente AchatVenteSelectionne
        {
            get { return _AchatVenteSelectionne; }
            set { AssignerChamp<C_AchatVente>(ref _AchatVenteSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnAchatVente _UnAchatVente;
        public VM_UnAchatVente UnAchatVente
        {
            get { return _UnAchatVente; }
            set { AssignerChamp<VM_UnAchatVente>(ref _UnAchatVente, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_AchatVente> _BcpAchatVente = new ObservableCollection<C_AchatVente>();
        public ObservableCollection<C_AchatVente> BcpAchatVente
        {
            get { return _BcpAchatVente; }
            set { _BcpAchatVente = value; }
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
        public VM_AchatVente()
        {
            UnAchatVente = new VM_UnAchatVente();
            string Index = UnAchatVente.IDOperation.ToString();
            BcpAchatVente = ChargerAchatVente(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_AchatVente> ChargerAchatVente(string chConn, string Index)
        {
            ObservableCollection<C_AchatVente> rep = new ObservableCollection<C_AchatVente>();
            List<C_AchatVente> lTmp = new Model.G_AchatVente(chConn).Lire(Index);
            foreach (C_AchatVente Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnAchatVente.IDOperation = new Model.G_AchatVente(chConnexion).Ajouter(UnAchatVente.IDVoiture, UnAchatVente.IDClient, UnAchatVente.Prix,UnAchatVente.Date,UnAchatVente.IDPaiement, UnAchatVente.Type);
                BcpAchatVente.Add(new C_AchatVente(UnAchatVente.IDOperation,UnAchatVente.IDVoiture, UnAchatVente.IDClient, UnAchatVente.Prix, UnAchatVente.Date, UnAchatVente.IDPaiement, UnAchatVente.Type));
            }
            else
            {
                new Model.G_AchatVente(chConnexion).Modifier(UnAchatVente.IDOperation, UnAchatVente.IDVoiture, UnAchatVente.IDClient, UnAchatVente.Prix, UnAchatVente.Date, UnAchatVente.IDPaiement, UnAchatVente.Type);
                BcpAchatVente[nAjout] = new C_AchatVente(UnAchatVente.IDOperation, UnAchatVente.IDVoiture, UnAchatVente.IDClient, UnAchatVente.Prix, UnAchatVente.Date, UnAchatVente.IDPaiement, UnAchatVente.Type);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnAchatVente = new VM_UnAchatVente();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (AchatVenteSelectionne != null)
            {
                C_AchatVente Tmp = new Model.G_AchatVente(chConnexion).Lire_ID(AchatVenteSelectionne.idOperation);
                UnAchatVente = new VM_UnAchatVente();
                UnAchatVente.IDOperation = Tmp.idOperation;
                UnAchatVente.IDVoiture = Tmp.idVoiture;
                UnAchatVente.IDClient = Tmp.idClient;
                UnAchatVente.Prix = Tmp.prixOperation;
                UnAchatVente.Date = Tmp.dateOperation;
                UnAchatVente.IDPaiement = Tmp.idPaiement;
                UnAchatVente.Type = Tmp.typeOperation;
                nAjout = BcpAchatVente.IndexOf(AchatVenteSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (AchatVenteSelectionne != null)
            {
                new Model.G_ClientsVoiture(chConnexion).Supprimer(AchatVenteSelectionne.idOperation);
                BcpAchatVente.Remove(AchatVenteSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_ClientsVoiture p in lTmp)
            { string s = p.nomClient; }
            int nTmp = lTmp.Count;
        }
        public void AchatVenteSelectionnee2UnAchatVente()
        {
            UnAchatVente.IDOperation = AchatVenteSelectionne.idOperation;
            UnAchatVente.IDVoiture = AchatVenteSelectionne.idVoiture;
            UnAchatVente.IDClient = AchatVenteSelectionne.idClient;
            UnAchatVente.Prix = AchatVenteSelectionne.prixOperation;
            UnAchatVente.Date = AchatVenteSelectionne.dateOperation;
            UnAchatVente.IDPaiement = AchatVenteSelectionne.idPaiement;
            UnAchatVente.Type = AchatVenteSelectionne.typeOperation;
        }
    }
    public class VM_UnAchatVente : BasePropriete
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
