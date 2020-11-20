#region Ressources extérieures
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion

namespace ISET2018_WPFBD.Model
{
	/// <summary>
	/// Couche d'accès aux données (Data Access Layer)
	/// </summary>
	public class A_Paiement : ADBase
	{
		#region Constructeurs
		public A_Paiement(string sChaineConnexion)
			: base(sChaineConnexion)
		{ }
		#endregion
		public int Ajouter(string nomPaiement)
		{
			CreerCommande("AjouterPaiementVoiture");
			int res = 0;
			Commande.Parameters.Add("idPaiement", SqlDbType.Int);
			Direction("idPaiement", ParameterDirection.Output);
			Commande.Parameters.AddWithValue("@nomPaiement", nomPaiement);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			res = int.Parse(LireParametre("idPaiement"));
			Commande.Connection.Close();
			return res;
		}
		public int Modifier(int idPaiement, string nomPaiement)
		{
			CreerCommande("ModifierPaiementVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idPaiement", idPaiement);
			Commande.Parameters.AddWithValue("@nomPaiement", nomPaiement);
			Commande.Connection.Open();
			Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
		public List<C_Paiement> Lire(string Index)
		{
			CreerCommande("SelectionnerPaiementVoiture");
			Commande.Parameters.AddWithValue("@Index", Index);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			List<C_Paiement> res = new List<C_Paiement>();
			while (dr.Read())
			{
				C_Paiement tmp = new C_Paiement();
				tmp.idPaiement = int.Parse(dr["idPaiement"].ToString());
				tmp.nomPaiement = dr["nomPaiement"].ToString();
				res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public C_Paiement Lire_ID(int idPaiement)
		{
			CreerCommande("SelectionnerPaiementVoiture_ID");
			Commande.Parameters.AddWithValue("@idPaiement", idPaiement);
			Commande.Connection.Open();
			SqlDataReader dr = Commande.ExecuteReader();
			C_Paiement res = new C_Paiement();
			while (dr.Read())
			{
				res.idPaiement = int.Parse(dr["idPaiement"].ToString());
				res.nomPaiement = dr["nomPaiement"].ToString();
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
		public int Supprimer(int idPaiement)
		{
			CreerCommande("SupprimerPaiementVoiture");
			int res = 0;
			Commande.Parameters.AddWithValue("@idPaiement", idPaiement);
			Commande.Connection.Open();
			res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
	}
}

