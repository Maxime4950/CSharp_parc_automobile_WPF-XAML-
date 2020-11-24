﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISET2018_WPFBD.DataAccess.DataObject
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="C:\\USERS\\MAESM\\DOCUMENTS\\COMPLEMENT_P\\ISET2018_WPFBD_MVVM_CONCEPT\\ISET2018_WPFBD\\" +
		"BD_VOITURE_MVVM.MDF")]
	public partial class CouleurDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Définitions de méthodes d'extensibilité
    partial void OnCreated();
    partial void InsertCouleurVoiture(CouleurVoiture instance);
    partial void UpdateCouleurVoiture(CouleurVoiture instance);
    partial void DeleteCouleurVoiture(CouleurVoiture instance);
    #endregion
		
		public CouleurDataContext() : 
				base(global::ISET2018_WPFBD.Properties.Settings.Default.C__USERS_MAESM_DOCUMENTS_COMPLEMENT_P_ISET2018_WPFBD_MVVM_CONCEPT_ISET2018_WPFBD_BD_VOITURE_MVVM_MDFConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CouleurDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CouleurDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CouleurDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CouleurDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CouleurVoiture> CouleurVoiture
		{
			get
			{
				return this.GetTable<CouleurVoiture>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CouleurVoiture")]
	public partial class CouleurVoiture : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _idCouleur;
		
		private string _nomCouleur;
		
    #region Définitions de méthodes d'extensibilité
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidCouleurChanging(int value);
    partial void OnidCouleurChanged();
    partial void OnnomCouleurChanging(string value);
    partial void OnnomCouleurChanged();
    #endregion
		
		public CouleurVoiture()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idCouleur", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int idCouleur
		{
			get
			{
				return this._idCouleur;
			}
			set
			{
				if ((this._idCouleur != value))
				{
					this.OnidCouleurChanging(value);
					this.SendPropertyChanging();
					this._idCouleur = value;
					this.SendPropertyChanged("idCouleur");
					this.OnidCouleurChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_nomCouleur", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string nomCouleur
		{
			get
			{
				return this._nomCouleur;
			}
			set
			{
				if ((this._nomCouleur != value))
				{
					this.OnnomCouleurChanging(value);
					this.SendPropertyChanging();
					this._nomCouleur = value;
					this.SendPropertyChanged("nomCouleur");
					this.OnnomCouleurChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591