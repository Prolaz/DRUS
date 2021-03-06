﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("ProjectDatabaseModel", "FK__STATIONS__LOCATI__08EA5793", "LOCATIONS", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(PubSubService.LOCATION), "STATIONS", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(PubSubService.STATION), true)]
[assembly: EdmRelationshipAttribute("ProjectDatabaseModel", "FK__MEASUREME__STATI__09DE7BCC", "STATIONS", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(PubSubService.STATION), "MEASUREMENTS", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(PubSubService.MEASUREMENT), true)]

#endregion

namespace PubSubService
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ProjectDatabaseEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ProjectDatabaseEntities object using the connection string found in the 'ProjectDatabaseEntities' section of the application configuration file.
        /// </summary>
        public ProjectDatabaseEntities() : base("name=ProjectDatabaseEntities", "ProjectDatabaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ProjectDatabaseEntities object.
        /// </summary>
        public ProjectDatabaseEntities(string connectionString) : base(connectionString, "ProjectDatabaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ProjectDatabaseEntities object.
        /// </summary>
        public ProjectDatabaseEntities(EntityConnection connection) : base(connection, "ProjectDatabaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<LOCATION> LOCATIONS
        {
            get
            {
                if ((_LOCATIONS == null))
                {
                    _LOCATIONS = base.CreateObjectSet<LOCATION>("LOCATIONS");
                }
                return _LOCATIONS;
            }
        }
        private ObjectSet<LOCATION> _LOCATIONS;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<MEASUREMENT> MEASUREMENTS
        {
            get
            {
                if ((_MEASUREMENTS == null))
                {
                    _MEASUREMENTS = base.CreateObjectSet<MEASUREMENT>("MEASUREMENTS");
                }
                return _MEASUREMENTS;
            }
        }
        private ObjectSet<MEASUREMENT> _MEASUREMENTS;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<STATION> STATIONS
        {
            get
            {
                if ((_STATIONS == null))
                {
                    _STATIONS = base.CreateObjectSet<STATION>("STATIONS");
                }
                return _STATIONS;
            }
        }
        private ObjectSet<STATION> _STATIONS;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the LOCATIONS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLOCATIONS(LOCATION lOCATION)
        {
            base.AddObject("LOCATIONS", lOCATION);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the MEASUREMENTS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMEASUREMENTS(MEASUREMENT mEASUREMENT)
        {
            base.AddObject("MEASUREMENTS", mEASUREMENT);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the STATIONS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSTATIONS(STATION sTATION)
        {
            base.AddObject("STATIONS", sTATION);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ProjectDatabaseModel", Name="LOCATION")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class LOCATION : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new LOCATION object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="nAME">Initial value of the NAME property.</param>
        public static LOCATION CreateLOCATION(global::System.Int32 id, global::System.String nAME)
        {
            LOCATION lOCATION = new LOCATION();
            lOCATION.ID = id;
            lOCATION.NAME = nAME;
            return lOCATION;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                OnNAMEChanging(value);
                ReportPropertyChanging("NAME");
                _NAME = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("NAME");
                OnNAMEChanged();
            }
        }
        private global::System.String _NAME;
        partial void OnNAMEChanging(global::System.String value);
        partial void OnNAMEChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ProjectDatabaseModel", "FK__STATIONS__LOCATI__08EA5793", "STATIONS")]
        public EntityCollection<STATION> STATIONS
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<STATION>("ProjectDatabaseModel.FK__STATIONS__LOCATI__08EA5793", "STATIONS");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<STATION>("ProjectDatabaseModel.FK__STATIONS__LOCATI__08EA5793", "STATIONS", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ProjectDatabaseModel", Name="MEASUREMENT")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class MEASUREMENT : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new MEASUREMENT object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="vALUE">Initial value of the VALUE property.</param>
        /// <param name="sTATION_ID">Initial value of the STATION_ID property.</param>
        /// <param name="tYPE">Initial value of the TYPE property.</param>
        /// <param name="tIME">Initial value of the TIME property.</param>
        public static MEASUREMENT CreateMEASUREMENT(global::System.Int32 id, global::System.Decimal vALUE, global::System.String sTATION_ID, global::System.String tYPE, global::System.DateTime tIME)
        {
            MEASUREMENT mEASUREMENT = new MEASUREMENT();
            mEASUREMENT.ID = id;
            mEASUREMENT.VALUE = vALUE;
            mEASUREMENT.STATION_ID = sTATION_ID;
            mEASUREMENT.TYPE = tYPE;
            mEASUREMENT.TIME = tIME;
            return mEASUREMENT;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal VALUE
        {
            get
            {
                return _VALUE;
            }
            set
            {
                OnVALUEChanging(value);
                ReportPropertyChanging("VALUE");
                _VALUE = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("VALUE");
                OnVALUEChanged();
            }
        }
        private global::System.Decimal _VALUE;
        partial void OnVALUEChanging(global::System.Decimal value);
        partial void OnVALUEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String STATION_ID
        {
            get
            {
                return _STATION_ID;
            }
            set
            {
                OnSTATION_IDChanging(value);
                ReportPropertyChanging("STATION_ID");
                _STATION_ID = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("STATION_ID");
                OnSTATION_IDChanged();
            }
        }
        private global::System.String _STATION_ID;
        partial void OnSTATION_IDChanging(global::System.String value);
        partial void OnSTATION_IDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String TYPE
        {
            get
            {
                return _TYPE;
            }
            set
            {
                OnTYPEChanging(value);
                ReportPropertyChanging("TYPE");
                _TYPE = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("TYPE");
                OnTYPEChanged();
            }
        }
        private global::System.String _TYPE;
        partial void OnTYPEChanging(global::System.String value);
        partial void OnTYPEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime TIME
        {
            get
            {
                return _TIME;
            }
            set
            {
                OnTIMEChanging(value);
                ReportPropertyChanging("TIME");
                _TIME = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TIME");
                OnTIMEChanged();
            }
        }
        private global::System.DateTime _TIME;
        partial void OnTIMEChanging(global::System.DateTime value);
        partial void OnTIMEChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ProjectDatabaseModel", "FK__MEASUREME__STATI__09DE7BCC", "STATIONS")]
        public STATION STATION
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<STATION>("ProjectDatabaseModel.FK__MEASUREME__STATI__09DE7BCC", "STATIONS").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<STATION>("ProjectDatabaseModel.FK__MEASUREME__STATI__09DE7BCC", "STATIONS").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<STATION> STATIONReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<STATION>("ProjectDatabaseModel.FK__MEASUREME__STATI__09DE7BCC", "STATIONS");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<STATION>("ProjectDatabaseModel.FK__MEASUREME__STATI__09DE7BCC", "STATIONS", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ProjectDatabaseModel", Name="STATION")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class STATION : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new STATION object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="nAME">Initial value of the NAME property.</param>
        /// <param name="lOCATION_ID">Initial value of the LOCATION_ID property.</param>
        public static STATION CreateSTATION(global::System.String id, global::System.String nAME, global::System.Int32 lOCATION_ID)
        {
            STATION sTATION = new STATION();
            sTATION.ID = id;
            sTATION.NAME = nAME;
            sTATION.LOCATION_ID = lOCATION_ID;
            return sTATION;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.String _ID;
        partial void OnIDChanging(global::System.String value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                OnNAMEChanging(value);
                ReportPropertyChanging("NAME");
                _NAME = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("NAME");
                OnNAMEChanged();
            }
        }
        private global::System.String _NAME;
        partial void OnNAMEChanging(global::System.String value);
        partial void OnNAMEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 LOCATION_ID
        {
            get
            {
                return _LOCATION_ID;
            }
            set
            {
                OnLOCATION_IDChanging(value);
                ReportPropertyChanging("LOCATION_ID");
                _LOCATION_ID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LOCATION_ID");
                OnLOCATION_IDChanged();
            }
        }
        private global::System.Int32 _LOCATION_ID;
        partial void OnLOCATION_IDChanging(global::System.Int32 value);
        partial void OnLOCATION_IDChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ProjectDatabaseModel", "FK__STATIONS__LOCATI__08EA5793", "LOCATIONS")]
        public LOCATION LOCATION
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<LOCATION>("ProjectDatabaseModel.FK__STATIONS__LOCATI__08EA5793", "LOCATIONS").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<LOCATION>("ProjectDatabaseModel.FK__STATIONS__LOCATI__08EA5793", "LOCATIONS").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<LOCATION> LOCATIONReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<LOCATION>("ProjectDatabaseModel.FK__STATIONS__LOCATI__08EA5793", "LOCATIONS");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<LOCATION>("ProjectDatabaseModel.FK__STATIONS__LOCATI__08EA5793", "LOCATIONS", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ProjectDatabaseModel", "FK__MEASUREME__STATI__09DE7BCC", "MEASUREMENTS")]
        public EntityCollection<MEASUREMENT> MEASUREMENTS
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<MEASUREMENT>("ProjectDatabaseModel.FK__MEASUREME__STATI__09DE7BCC", "MEASUREMENTS");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<MEASUREMENT>("ProjectDatabaseModel.FK__MEASUREME__STATI__09DE7BCC", "MEASUREMENTS", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
