using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISET2018_WPFBD.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace ISET2018_WPFBD.ViewModel
{
    public class VM_Personne : BasePropriete
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
        private C_ClientsVoiture _PersonneSelectionnee;
        public C_ClientsVoiture PersonneSelectionnee
        {
            get { return _PersonneSelectionnee; }
            set { AssignerChamp<C_ClientsVoiture>(ref _PersonneSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
      #endregion

        #region Données extérieures
        private VM_UnePersonne _UnePersonne;
        public VM_UnePersonne UnePersonne
        {
            get { return _UnePersonne; }
            set { AssignerChamp<VM_UnePersonne>(ref _UnePersonne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_ClientsVoiture> _BcpPersonnes = new ObservableCollection<C_ClientsVoiture>();
         public ObservableCollection<C_ClientsVoiture> BcpPersonnes
         {
            get { return _BcpPersonnes; }
            set { _BcpPersonnes = value; }
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
        public VM_Personne()
        {
           UnePersonne = new VM_UnePersonne();
           UnePersonne.ID = 24;
           UnePersonne.Pre = "Largo";
           UnePersonne.Nom = "Winch";
           UnePersonne.Rue = "Rue du film";
           UnePersonne.Numero = 13;
           UnePersonne.Boite = 3;
           UnePersonne.CodePo = 4950;
           UnePersonne.Localite = "Sourbrodt";
           string Index = UnePersonne.ID.ToString();
           BcpPersonnes = ChargerPersonnes(chConnexion, Index);
           ActiverUneFiche = false;
           cConfirmer = new BaseCommande(Confirmer);
           cAnnuler = new BaseCommande(Annuler);
           cAjouter = new BaseCommande(Ajouter);
           cModifier = new BaseCommande(Modifier);
           cSupprimer = new BaseCommande(Supprimer);
           cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        //Pour charger les clients afin de les afficher dans le dgv
        private ObservableCollection<C_ClientsVoiture> ChargerPersonnes(string chConn, string Index)
        {
           ObservableCollection<C_ClientsVoiture> rep = new ObservableCollection<C_ClientsVoiture>();
           List<C_ClientsVoiture> lTmp = new Model.G_ClientsVoiture(chConn).Lire(Index);
           foreach (C_ClientsVoiture Tmp in lTmp)
           rep.Add(Tmp);
           return rep;
        }

        //Confirmation pour l'ajout ou la modification
        public void Confirmer()
        {
        if (nAjout == -1)
        {
            UnePersonne.ID = new Model.G_ClientsVoiture(chConnexion).Ajouter(UnePersonne.Nom, UnePersonne.Pre, UnePersonne.Rue, UnePersonne.Numero, UnePersonne.Boite, UnePersonne.CodePo, UnePersonne.Localite);
            BcpPersonnes.Add(new C_ClientsVoiture(UnePersonne.ID,UnePersonne.Nom, UnePersonne.Pre, UnePersonne.Rue, UnePersonne.Numero, UnePersonne.Boite, UnePersonne.CodePo, UnePersonne.Localite));
        }
        else
        {
            new Model.G_ClientsVoiture(chConnexion).Modifier(UnePersonne.ID, UnePersonne.Nom, UnePersonne.Pre, UnePersonne.Rue, UnePersonne.Numero, UnePersonne.Boite, UnePersonne.CodePo, UnePersonne.Localite);
            BcpPersonnes[nAjout] = new C_ClientsVoiture(UnePersonne.ID, UnePersonne.Nom, UnePersonne.Pre, UnePersonne.Rue, UnePersonne.Numero, UnePersonne.Boite, UnePersonne.CodePo, UnePersonne.Localite);
        }
        ActiverUneFiche = false;
        }
        public void Annuler()
        { 
            ActiverUneFiche = false;
        }
        public void Ajouter()
        {
            MessageBox.Show("test");
            UnePersonne = new VM_UnePersonne();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
        if (PersonneSelectionnee != null)
        {
            C_ClientsVoiture Tmp = new Model.G_ClientsVoiture(chConnexion).Lire_ID(PersonneSelectionnee.idClient);
            UnePersonne = new VM_UnePersonne();
            UnePersonne.ID = Tmp.idClient;
            UnePersonne.Pre = Tmp.prenomClient;
            UnePersonne.Nom = Tmp.nomClient;
            UnePersonne.Rue = Tmp.rueClient;
            UnePersonne.Numero = Tmp.numeroClient;
            UnePersonne.Boite = Tmp.boiteClient;
            UnePersonne.CodePo = Tmp.codePoClient;
            UnePersonne.Localite = Tmp.localiteClient;
            nAjout = BcpPersonnes.IndexOf(PersonneSelectionnee);
            ActiverUneFiche = true;
        }
        }
        public void Supprimer()
        {
        if (PersonneSelectionnee != null)
        {
            MessageBox.Show(PersonneSelectionnee.idClient.ToString());
            new Model.G_ClientsVoiture(chConnexion).Supprimer(PersonneSelectionnee.idClient);
            BcpPersonnes.Remove(PersonneSelectionnee);
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
           UnePersonne.ID = PersonneSelectionnee.idClient;
           UnePersonne.Nom = PersonneSelectionnee.nomClient;
           UnePersonne.Pre = PersonneSelectionnee.prenomClient;
           UnePersonne.Rue = PersonneSelectionnee.rueClient;
           UnePersonne.Numero = PersonneSelectionnee.numeroClient;
           UnePersonne.Boite = PersonneSelectionnee.boiteClient;
           UnePersonne.CodePo = PersonneSelectionnee.codePoClient;
           UnePersonne.Localite = PersonneSelectionnee.localiteClient;
        }
    }
    public class VM_UnePersonne : BasePropriete
    {
        private int _ID, _Numero, _CodePo;
        private int? _Boite;
        private string _Nom, _Pre, _Rue, _Localite;
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

        public string Pre
        {
            get { return _Pre; }
            set { AssignerChamp<string>(ref _Pre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Rue
        {
            get { return _Rue; }
            set { AssignerChamp<string>(ref _Rue, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Localite
        {
            get { return _Localite; }
            set { AssignerChamp<string>(ref _Localite, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int Numero
        {
            get { return _Numero; }
            set { AssignerChamp<int>(ref _Numero, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? Boite
        {
            get { return _Boite; }
            set { AssignerChamp<int?>(ref _Boite, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int CodePo
        {
            get { return _CodePo; }
            set { AssignerChamp<int>(ref _CodePo, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}
