using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ISET2018_WPFBD.Model
{
    public class A_Categorie : ADBase
    {
		#region Constructeurs
		public A_Categorie(string sChaineConnexion)
			: base(sChaineConnexion)
		{ }
		#endregion
		public int Ajouter(string nomCat)
		{
			CreerCommande("AjouterCategorieVoiture");
			int res = 0;
			Commande.Parameters.Add("idCat", SqlDbType.Int);
			Direction("idCat", ParameterDirection.Output);
			if (nomCat == null) Commande.Parameters.AddWithValue("@nomCat", Convert.DBNull);
			else Commande.Parameters.AddWithValue("@nomCat", nomCat);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			res = int.Parse(LireParametre("idCat"));
			Commande.Connection.Close();
			return res;
		}
		public int Modifier(int idCat, string nomCat)
		{
			CreerCommande("ModifierCategorieVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idCat", idCat);
			if (nomCat == null) Commande.Parameters.AddWithValue("@nomCat", Convert.DBNull);
			else Commande.Parameters.AddWithValue("@nomCat", nomCat);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
		public List<C_Categorie> Lire(string Index)
		{
			CreerCommande("SelectionnerCategorieVoiture");
			Commande.Parameters.AddWithValue("@Index", Index);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			List<C_Categorie> res = new List<C_Categorie>();
			while (dr.Read())
			{
				C_Categorie tmp = new C_Categorie();
				tmp.idCat = int.Parse(dr["idCat"].ToString());
				tmp.nomCat = dr["nomCat"].ToString();
				res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public C_Categorie Lire_ID(int idCat)
		{
			CreerCommande("SelectionnerCategorieVoiture_ID");
			Commande.Parameters.AddWithValue("@idCat", idCat);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			C_Categorie res = new C_Categorie();
			while (dr.Read())
			{
				res.idCat = int.Parse(dr["idCat"].ToString());
				res.nomCat = dr["nomCat"].ToString();
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public int Supprimer(int idCat)
		{
			CreerCommande("SupprimerCategorieVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idCat", idCat);
			Commande.Connection.Open();
			res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
	}
}
