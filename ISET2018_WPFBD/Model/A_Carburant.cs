using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ISET2018_WPFBD.Model
{
    public class A_Carburant : ADBase
    {
		#region Constructeurs
		public A_Carburant(string sChaineConnexion)
			: base(sChaineConnexion)
		{ }
		#endregion
		public int Ajouter(string nomCarburant)
		{
			CreerCommande("AjouterCarburantVoiture");
			int res = 0;
			Commande.Parameters.Add("idCarburant", SqlDbType.Int);
			Direction("idCarburant", ParameterDirection.Output);
			Commande.Parameters.AddWithValue("@nomCarburant", nomCarburant);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			res = int.Parse(LireParametre("idCarburant"));
			Commande.Connection.Close();
			return res;
		}
		public int Modifier(int idCarburant, string nomCarburant)
		{
			CreerCommande("ModifierCarburantVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idCarburant", idCarburant);
			Commande.Parameters.AddWithValue("@nomCarburant", nomCarburant);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
		public List<C_Carburant> Lire(string Index)
		{
			CreerCommande("SelectionnerCarburantVoiture");
			Commande.Parameters.AddWithValue("@Index", Index);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			List<C_Carburant> res = new List<C_Carburant>();
			while (dr.Read())
			{
				C_Carburant tmp = new C_Carburant();
				tmp.idCarburant = int.Parse(dr["idCarburant"].ToString());
				tmp.nomCarburant = dr["nomCarburant"].ToString();
				res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public C_Carburant Lire_ID(int idCarburant)
		{
			CreerCommande("SelectionnerCarburantVoiture_ID");
			Commande.Parameters.AddWithValue("@idCarburant", idCarburant);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			C_Carburant res = new C_Carburant();
			while (dr.Read())
			{
				res.idCarburant = int.Parse(dr["idCarburant"].ToString());
				res.nomCarburant = dr["nomCarburant"].ToString();
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public int Supprimer(int idCarburant)
		{
			CreerCommande("SupprimerCarburantVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idCarburant", idCarburant);
			Commande.Connection.Open();
			res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
	}
}
