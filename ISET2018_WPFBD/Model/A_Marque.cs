using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ISET2018_WPFBD.Model
{
    public class A_Marque : ADBase
    {
        #region Constructeurs
        public A_Marque(string sChaineConnexion)
            : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string nomMarque, string paysMarque)
        {
            CreerCommande("AjouterMarqueVoiture");
            int res = 0;
            Commande.Parameters.Add("idMarque", SqlDbType.Int);
            Direction("idMarque", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@nomMarque", nomMarque);
            Commande.Parameters.AddWithValue("@paysMarque", paysMarque);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("idMarque"));
            Commande.Connection.Close();
            return res;
        }
        public int Modifier(int idMarque, string nomMarque, string paysMarque)
        {
            CreerCommande("ModifierMarqueVoiture");
            int res = 0;
            Commande.Parameters.AddWithValue("@idMarque", idMarque);
            Commande.Parameters.AddWithValue("@nomMarque", nomMarque);
            Commande.Parameters.AddWithValue("@paysMarque", paysMarque);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
        public List<C_Marque> Lire(string Index)
        {
            CreerCommande("SelectionnerMarqueVoiture");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_Marque> res = new List<C_Marque>();
            while (dr.Read())
            {
                C_Marque tmp = new C_Marque();
                tmp.idMarque = int.Parse(dr["idMarque"].ToString());
                tmp.nomMarque = dr["nomMarque"].ToString();
                tmp.paysMarque = dr["paysMarque"].ToString();
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public C_Marque Lire_ID(int idMarque)
        {
            CreerCommande("SelectionnerMarqueVoiture_ID");
            Commande.Parameters.AddWithValue("@idMarque", idMarque);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_Marque res = new C_Marque();
            while (dr.Read())
            {
                res.idMarque = int.Parse(dr["idMarque"].ToString());
                res.nomMarque = dr["nomMarque"].ToString();
                res.paysMarque = dr["paysMarque"].ToString();
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public int Supprimer(int idMarque)
        {
            CreerCommande("SupprimerMarqueVoiture");
            int res = 0;
            Commande.Parameters.AddWithValue("@idMarque", idMarque);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
    }
}
