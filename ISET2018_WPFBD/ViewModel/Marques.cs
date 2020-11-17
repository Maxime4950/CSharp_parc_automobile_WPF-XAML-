using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Marques : BasePropriete
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
        private C_Marque _MarqueSelectionnee;
        public C_Marque MarqueSelectionnee
        {
            get { return _MarqueSelectionnee; }
            set { AssignerChamp<C_Marque>(ref _MarqueSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UneMarque _UneMarque;
        public VM_UneMarque UneMarque
        {
            get { return _UneMarque; }
            set { AssignerChamp<VM_UneMarque>(ref _UneMarque, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_Marque> _BcpMarques = new ObservableCollection<C_Marque>();
        public ObservableCollection<C_Marque> BcpMarques
        {
            get { return _BcpMarques; }
            set { _BcpMarques = value; }
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
        public VM_Marques()
        {
            UneMarque = new VM_UneMarque();
            UneMarque.ID = 34;
            UneMarque.Nom = "Peugeot";
            UneMarque.Pays = "France";

            string Index = UneMarque.ID.ToString();
            BcpMarques = ChargerMarques(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_Marque> ChargerMarques(string chConn, string Index)
        {
            ObservableCollection<C_Marque> rep = new ObservableCollection<C_Marque>();
            List<C_Marque> lTmp = new Model.G_Marque(chConn).Lire(Index);
            foreach (C_Marque Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneMarque.ID = new Model.G_Marque(chConnexion).Ajouter(UneMarque.Nom, UneMarque.Pays);
                BcpMarques.Add(new C_Marque(UneMarque.ID,UneMarque.Nom, UneMarque.Pays));
            }
            else
            {
                new Model.G_Marque(chConnexion).Modifier(UneMarque.ID,UneMarque.Nom, UneMarque.Pays);
                BcpMarques[nAjout] = new C_Marque(UneMarque.ID, UneMarque.Nom, UneMarque.Pays);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UneMarque = new VM_UneMarque();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (MarqueSelectionnee != null)
            {
                C_Marque Tmp = new Model.G_Marque(chConnexion).Lire_ID(MarqueSelectionnee.idMarque);
                UneMarque = new VM_UneMarque();
                UneMarque.ID = Tmp.idMarque;
                UneMarque.Nom = Tmp.nomMarque;
                UneMarque.Pays = Tmp.paysMarque;
                nAjout = BcpMarques.IndexOf(MarqueSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (MarqueSelectionnee != null)
            {
                new Model.G_Marque(chConnexion).Supprimer(MarqueSelectionnee.idMarque);
                BcpMarques.Remove(MarqueSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_Marque p in lTmp)
            { string s = p.nomMarque; }
            int nTmp = lTmp.Count;
        }
        public void MarqueSelectionnee2UneMarque()
        {
            UneMarque.ID = MarqueSelectionnee.idMarque;
            UneMarque.Nom = MarqueSelectionnee.nomMarque;
            UneMarque.Pays = MarqueSelectionnee.paysMarque;
        }
    }

    public class VM_UneMarque : BasePropriete
    {
        private int _ID;
        private string _Nom, _Pays;
        public int ID
        {
            get { return _ID; }
            set { AssignerChamp<int>(ref _ID, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Nom
        {
            get { return _Nom; }
            set { AssignerChamp<string>(ref _Nom, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Pays
        {
            get { return _Pays; }
            set { AssignerChamp<string>(ref _Pays, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}
