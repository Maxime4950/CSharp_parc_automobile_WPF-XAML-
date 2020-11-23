using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Modeles : BasePropriete
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
        private C_Modele _ModeleSelectionnee;
        public C_Modele ModeleSelectionnee
        {
            get { return _ModeleSelectionnee; }
            set { AssignerChamp<C_Modele>(ref _ModeleSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion

        #region Données extérieures
        private VM_UnModele _UnModele;
        public VM_UnModele UnModele
        {
            get { return _UnModele; }
            set { AssignerChamp<VM_UnModele>(ref _UnModele, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_Modele> _BcpModeles = new ObservableCollection<C_Modele>();
        public ObservableCollection<C_Modele> BcpModeles
        {
            get { return _BcpModeles; }
            set { _BcpModeles = value; }
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
        public VM_Modeles()
        {
            UnModele = new VM_UnModele();
            UnModele.ID = 34;
            UnModele.Nom = "Xantia";

            string Index = UnModele.ID.ToString();
            BcpModeles = ChargerModeles(chConnexion, Index);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_Modele> ChargerModeles(string chConn, string Index)
        {
            ObservableCollection<C_Modele> rep = new ObservableCollection<C_Modele>();
            List<C_Modele> lTmp = new Model.G_Modele(chConn).Lire(Index);
            foreach (C_Modele Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnModele.ID = new Model.G_Modele(chConnexion).Ajouter(UnModele.Nom);
                BcpModeles.Add(new C_Modele(UnModele.ID, UnModele.Nom));
            }
            else
            {
                new Model.G_Modele(chConnexion).Modifier(UnModele.ID, UnModele.Nom);
                BcpModeles[nAjout] = new C_Modele(UnModele.ID, UnModele.Nom);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        {
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            UnModele = new VM_UnModele();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (ModeleSelectionnee != null)
            {
                C_Modele Tmp = new Model.G_Modele(chConnexion).Lire_ID(ModeleSelectionnee.idModele);
                UnModele = new VM_UnModele();
                UnModele.ID = Tmp.idModele;
                UnModele.Nom = Tmp.nomModele;
                nAjout = BcpModeles.IndexOf(ModeleSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (ModeleSelectionnee != null)
            {
                new Model.G_Modele(chConnexion).Supprimer(ModeleSelectionnee.idModele);
                BcpModeles.Remove(ModeleSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_Modele p in lTmp)
            { string s = p.nomModele; }
            int nTmp = lTmp.Count;
        }
        public void ModeleSelectionnee2UnModele()
        {
            UnModele.ID = ModeleSelectionnee.idModele;
            UnModele.Nom = ModeleSelectionnee.nomModele;
        }
    }

    public class VM_UnModele : BasePropriete
    {
        private int _ID;
        private string _Nom;
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

    }
}
