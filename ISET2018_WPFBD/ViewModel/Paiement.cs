using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Paiement : BasePropriete
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
        private C_Paiement _PaiementSelectionne;
        public C_Paiement PaiementSelectionne
        {
            get { return _PaiementSelectionne; }
            set { AssignerChamp<C_Paiement>(ref _PaiementSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnPaiement _UnPaiement;
        public VM_UnPaiement UnPaiement
        {
            get { return _UnPaiement; }
            set { AssignerChamp<VM_UnPaiement>(ref _UnPaiement, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_Paiement> _BcpPaiement = new ObservableCollection<C_Paiement>();
        public ObservableCollection<C_Paiement> BcpPaiement
        {
            get { return _BcpPaiement; }
            set { _BcpPaiement = value; }
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
        public VM_Paiement()
        {
            UnPaiement = new VM_UnPaiement();
            string Index = UnPaiement.ID.ToString();
            BcpPaiement = ChargerPaiement(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_Paiement> ChargerPaiement(string chConn, string Index)
        {
            ObservableCollection<C_Paiement> rep = new ObservableCollection<C_Paiement>();
            List<C_Paiement> lTmp = new Model.G_Paiement(chConn).Lire(Index);
            foreach (C_Paiement Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnPaiement.ID = new Model.G_Paiement(chConnexion).Ajouter(UnPaiement.Nom);
                BcpPaiement.Add(new C_Paiement(UnPaiement.ID, UnPaiement.Nom));
            }
            else
            {
                new Model.G_Paiement(chConnexion).Modifier(UnPaiement.ID, UnPaiement.Nom);
                BcpPaiement[nAjout] = new C_Paiement(UnPaiement.Nom);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnPaiement = new VM_UnPaiement();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (PaiementSelectionne != null)
            {
                C_Paiement Tmp = new Model.G_Paiement(chConnexion).Lire_ID(PaiementSelectionne.idPaiement);
                UnPaiement = new VM_UnPaiement();
                UnPaiement.ID = Tmp.idPaiement;
                UnPaiement.Nom = Tmp.nomPaiement;

                nAjout = BcpPaiement.IndexOf(PaiementSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (PaiementSelectionne != null)
            {
                new Model.G_Paiement(chConnexion).Supprimer(PaiementSelectionne.idPaiement);
                BcpPaiement.Remove(PaiementSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_ClientsVoiture p in lTmp)
            { string s = p.nomClient; }
            int nTmp = lTmp.Count;
        }
        public void PersonneSelectionnee2UnePersonne()
        {
            UnPaiement.ID = PaiementSelectionne.idPaiement;
            UnPaiement.Nom = PaiementSelectionne.nomPaiement;
        }
    }
    public class VM_UnPaiement : BasePropriete
    {
        private int _IDPaiement;
        private string _Nom;
        public int ID
        {
            get { return _IDPaiement; }
            set { AssignerChamp<int>(ref _IDPaiement, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Nom
        {
            get { return _Nom; }
            set { AssignerChamp<string>(ref _Nom, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}
