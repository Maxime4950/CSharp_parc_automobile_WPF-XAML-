using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ISET2018_WPFBD.Model
{
    public class A_Frais : ADBase
    {
        #region Constructeurs
        public A_Frais(string sChaineConnexion)
            : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(int idVoiture, string nomFrais, string descriptionFrais, int coutFrais)
        {
            CreerCommande("AjouterFraisVoiture");
            int res = 0;
            Commande.Parameters.Add("idFrais", SqlDbType.Int);
            Direction("idFrais", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@idVoiture", idVoiture);
            Commande.Parameters.AddWithValue("@nomFrais", nomFrais);
            Commande.Parameters.AddWithValue("@descriptionFrais", descriptionFrais);
            Commande.Parameters.AddWithValue("@coutFrais", coutFrais);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("idFrais"));
            Commande.Connection.Close();
            return res;
        }
        public int Modifier(int idFrais,int idVoiture, string nomFrais, string descriptionFrais, int coutFrais)
        {
            CreerCommande("ModifierFraisVoiture");
            int res = 0;
            Commande.Parameters.AddWithValue("@idFrais", idFrais);
            Commande.Parameters.AddWithValue("@idVoiture", idVoiture);
            Commande.Parameters.AddWithValue("@nomFrais", nomFrais);
            Commande.Parameters.AddWithValue("@descriptionFrais", descriptionFrais);
            Commande.Parameters.AddWithValue("@coutFrais", coutFrais);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
        public List<C_Frais> Lire(string Index)
        {
            CreerCommande("SelectionnerFraisVoiture");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_Frais> res = new List<C_Frais>();
            while (dr.Read())
            {
                C_Frais tmp = new C_Frais();
                tmp.idFrais = int.Parse(dr["idFrais"].ToString());
                tmp.idVoiture = int.Parse(dr["idVoiture"].ToString());
                tmp.nomFrais = dr["nomFrais"].ToString();
                tmp.descriptionFrais = dr["descriptionFrais"].ToString();
                tmp.coutFrais = int.Parse(dr["coutFrais"].ToString());
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public C_Frais Lire_ID(int idFrais)
        {
            CreerCommande("SelectionnerFraisVoiture_ID");
            Commande.Parameters.AddWithValue("@idFrais", idFrais);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_Frais res = new C_Frais();
            while (dr.Read())
            {
                res.idFrais = int.Parse(dr["idFrais"].ToString());
                res.idVoiture = int.Parse(dr["idVoiture"].ToString());
                res.nomFrais = dr["nomFrais"].ToString();
                res.descriptionFrais = dr["descriptionFrais"].ToString();
                res.coutFrais = int.Parse(dr["coutFrais"].ToString());
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public int Supprimer(int idFrais)
        {
            CreerCommande("SupprimerFraisVoiture");
            int res = 0;
            Commande.Parameters.AddWithValue("@idFrais", idFrais);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
    }
}
