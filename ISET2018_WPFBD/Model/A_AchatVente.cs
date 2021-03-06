﻿#region Ressources extérieures
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
    public class A_AchatVente : ADBase
    {
        #region Constructeurs
        public A_AchatVente(string sChaineConnexion)
            : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(int idVoiture, int idClient, int prixOperation, DateTime dateOperation, int idPaiement, string typeOperation)
        {
            CreerCommande("AjouterAchatVenteVoiture");
            int res = 0;
            Commande.Parameters.Add("idOperation", SqlDbType.Int);
            Direction("idOperation", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@idVoiture", idVoiture);
            Commande.Parameters.AddWithValue("@idClient", idClient);
            Commande.Parameters.AddWithValue("@prixOperation", prixOperation);
            Commande.Parameters.AddWithValue("@dateOperation", dateOperation);
            Commande.Parameters.AddWithValue("@idPaiement", idPaiement);
            Commande.Parameters.AddWithValue("@typeOperation", typeOperation);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("idOperation"));
            Commande.Connection.Close();
            return res;
        }
        public int Modifier(int idOperation, int idVoiture, int idClient, int prixOperation, DateTime dateOperation, int idPaiement, string typeOperation)
        {
            CreerCommande("ModifierAchatVenteVoiture");
            int res = 0;
            Commande.Parameters.AddWithValue("@idOperation", idOperation);
            Commande.Parameters.AddWithValue("@idVoiture", idVoiture);
            Commande.Parameters.AddWithValue("@idClient", idClient);
            Commande.Parameters.AddWithValue("@prixOperation", prixOperation);
            Commande.Parameters.AddWithValue("@dateOperation", dateOperation);
            Commande.Parameters.AddWithValue("@idPaiement", idPaiement);
            Commande.Parameters.AddWithValue("@typeOperation", typeOperation);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
        public List<C_AchatVente> Lire(string Index)
        {
            CreerCommande("SelectionnerAchatVenteVoiture");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_AchatVente> res = new List<C_AchatVente>();
            while (dr.Read())
            {
                C_AchatVente tmp = new C_AchatVente();
                tmp.idOperation = int.Parse(dr["idOperation"].ToString());
                tmp.idVoiture = int.Parse(dr["idVoiture"].ToString());
                tmp.idClient = int.Parse(dr["idClient"].ToString());
                tmp.prixOperation = int.Parse(dr["prixOperation"].ToString());
                tmp.dateOperation = DateTime.Parse(dr["dateOperation"].ToString());
                tmp.idPaiement = int.Parse(dr["idPaiement"].ToString());
                tmp.typeOperation = dr["typeOperation"].ToString();
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public C_AchatVente Lire_ID(int idOperation)
        {
            CreerCommande("SelectionnerAchatVenteVoiture_ID");
            Commande.Parameters.AddWithValue("@idOperation", idOperation);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_AchatVente res = new C_AchatVente();
            while (dr.Read())
            {
                res.idOperation = int.Parse(dr["idOperation"].ToString());
                res.idVoiture = int.Parse(dr["idVoiture"].ToString());
                res.idClient = int.Parse(dr["idClient"].ToString());
                res.prixOperation = int.Parse(dr["prixOperation"].ToString());
                res.dateOperation = DateTime.Parse(dr["dateOperation"].ToString());
                res.idPaiement = int.Parse(dr["idPaiement"].ToString());
                res.typeOperation = dr["typeOperation"].ToString();
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public int Supprimer(int idOperation)
        {
            CreerCommande("SupprimerAchatVenteVoiture");
            int res = 0;
            Commande.Parameters.AddWithValue("@idOperation", idOperation);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
    }
}
