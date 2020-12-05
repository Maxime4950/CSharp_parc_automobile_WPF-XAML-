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
	public partial class ClientDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Définitions de méthodes d'extensibilité
    partial void OnCreated();
    partial void InsertClientsVoiture(ClientsVoiture instance);
    partial void UpdateClientsVoiture(ClientsVoiture instance);
    partial void DeleteClientsVoiture(ClientsVoiture instance);
    #endregion
		
		public ClientDataContext() : 
				base(global::ISET2018_WPFBD.Properties.Settings.Default.C__USERS_MAESM_DOCUMENTS_COMPLEMENT_P_ISET2018_WPFBD_MVVM_CONCEPT_ISET2018_WPFBD_BD_VOITURE_MVVM_MDFConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ClientDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ClientDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ClientDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ClientDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ClientsVoiture> ClientsVoiture
		{
			get
			{
				return this.GetTable<ClientsVoiture>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ClientsVoiture")]
	public partial class ClientsVoiture : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _idClient;
		
		private string _nomClient;
		
		private string _prenomClient;
		
		private string _rueClient;
		
		private int _numeroClient;
		
		private System.Nullable<int> _boiteClient;
		
		private int _codePoClient;
		
		private string _localiteClient;
		
    #region Définitions de méthodes d'extensibilité
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidClientChanging(int value);
    partial void OnidClientChanged();
    partial void OnnomClientChanging(string value);
    partial void OnnomClientChanged();
    partial void OnprenomClientChanging(string value);
    partial void OnprenomClientChanged();
    partial void OnrueClientChanging(string value);
    partial void OnrueClientChanged();
    partial void OnnumeroClientChanging(int value);
    partial void OnnumeroClientChanged();
    partial void OnboiteClientChanging(System.Nullable<int> value);
    partial void OnboiteClientChanged();
    partial void OncodePoClientChanging(int value);
    partial void OncodePoClientChanged();
    partial void OnlocaliteClientChanging(string value);
    partial void OnlocaliteClientChanged();
    #endregion
		
		public ClientsVoiture()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idClient", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int idClient
		{
			get
			{
				return this._idClient;
			}
			set
			{
				if ((this._idClient != value))
				{
					this.OnidClientChanging(value);
					this.SendPropertyChanging();
					this._idClient = value;
					this.SendPropertyChanged("idClient");
					this.OnidClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_nomClient", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string nomClient
		{
			get
			{
				return this._nomClient;
			}
			set
			{
				if ((this._nomClient != value))
				{
					this.OnnomClientChanging(value);
					this.SendPropertyChanging();
					this._nomClient = value;
					this.SendPropertyChanged("nomClient");
					this.OnnomClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_prenomClient", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string prenomClient
		{
			get
			{
				return this._prenomClient;
			}
			set
			{
				if ((this._prenomClient != value))
				{
					this.OnprenomClientChanging(value);
					this.SendPropertyChanging();
					this._prenomClient = value;
					this.SendPropertyChanged("prenomClient");
					this.OnprenomClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rueClient", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string rueClient
		{
			get
			{
				return this._rueClient;
			}
			set
			{
				if ((this._rueClient != value))
				{
					this.OnrueClientChanging(value);
					this.SendPropertyChanging();
					this._rueClient = value;
					this.SendPropertyChanged("rueClient");
					this.OnrueClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_numeroClient", DbType="Int NOT NULL")]
		public int numeroClient
		{
			get
			{
				return this._numeroClient;
			}
			set
			{
				if ((this._numeroClient != value))
				{
					this.OnnumeroClientChanging(value);
					this.SendPropertyChanging();
					this._numeroClient = value;
					this.SendPropertyChanged("numeroClient");
					this.OnnumeroClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_boiteClient", DbType="Int")]
		public System.Nullable<int> boiteClient
		{
			get
			{
				return this._boiteClient;
			}
			set
			{
				if ((this._boiteClient != value))
				{
					this.OnboiteClientChanging(value);
					this.SendPropertyChanging();
					this._boiteClient = value;
					this.SendPropertyChanged("boiteClient");
					this.OnboiteClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codePoClient", DbType="Int NOT NULL")]
		public int codePoClient
		{
			get
			{
				return this._codePoClient;
			}
			set
			{
				if ((this._codePoClient != value))
				{
					this.OncodePoClientChanging(value);
					this.SendPropertyChanging();
					this._codePoClient = value;
					this.SendPropertyChanged("codePoClient");
					this.OncodePoClientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_localiteClient", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string localiteClient
		{
			get
			{
				return this._localiteClient;
			}
			set
			{
				if ((this._localiteClient != value))
				{
					this.OnlocaliteClientChanging(value);
					this.SendPropertyChanging();
					this._localiteClient = value;
					this.SendPropertyChanged("localiteClient");
					this.OnlocaliteClientChanged();
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
