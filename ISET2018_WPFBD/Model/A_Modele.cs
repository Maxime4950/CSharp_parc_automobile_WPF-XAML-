using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ISET2018_WPFBD.Model
{
    public class A_Modele : ADBase
	{
		#region Constructeurs
		public A_Modele(string sChaineConnexion)
			: base(sChaineConnexion)
		{ }
		#endregion
		public int Ajouter(string nomModele)
		{
			CreerCommande("AjouterModeleVoiture");
			int res = 0;
			Commande.Parameters.Add("idModele", SqlDbType.Int);
			Direction("idModele", ParameterDirection.Output);
			Commande.Parameters.AddWithValue("@nomModele", nomModele);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			res = int.Parse(LireParametre("idModele"));
			Commande.Connection.Close();
			return res;
		}
		public int Modifier(int idModele, string nomModele)
		{
			CreerCommande("ModifierModeleVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idModele", idModele);
			Commande.Parameters.AddWithValue("@nomModele", nomModele);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
		public List<C_Modele> Lire(string Index)
		{
			CreerCommande("SelectionnerModeleVoiture");
			Commande.Parameters.AddWithValue("@Index", Index);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			List<C_Modele> res = new List<C_Modele>();
			while (dr.Read())
			{
				C_Modele tmp = new C_Modele();
				tmp.idModele = int.Parse(dr["idModele"].ToString());
				tmp.nomModele = dr["nomModele"].ToString();
				res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public C_Modele Lire_ID(int idModele)
		{
			CreerCommande("SelectionnerModeleVoiture_ID");
			Commande.Parameters.AddWithValue("@idModele", idModele);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			C_Modele res = new C_Modele();
			while (dr.Read())
			{
				res.idModele = int.Parse(dr["idModele"].ToString());
				res.nomModele = dr["nomModele"].ToString();
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public int Supprimer(int idModele)
		{
			CreerCommande("SupprimerModeleVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idModele", idModele);
			Commande.Connection.Open();
			res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
	}
}
