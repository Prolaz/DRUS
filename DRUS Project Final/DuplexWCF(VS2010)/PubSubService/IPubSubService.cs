using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PubSubService
{
    [ServiceContract(Namespace = "http://ListPublishSubscribe.Service",
    SessionMode = SessionMode.Required, CallbackContract = typeof(IPubSubContract))]
    public interface IPubSubService
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        string Subscribe(string ID);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        string Unsubscribe(string ID);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void UnsubscribeAll();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<string> ListAllPublishers();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<string> ListMyPublishers();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<string> ListAllLocations();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        string PublisherInit(string Ime, string Lokacija);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void ClientInit();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void PublishValueChange(string Id, string Type, int Value);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<Measurement> AllMeasurementsFromTo(string ID, DateTime start, DateTime end);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<Measurement> CertainMeasurementFromTo(string ID, string Type, DateTime start, DateTime end);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<DateTime> HighLowByID(string ID, string Type, bool Hi, bool Lo, int Min, int Max);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        decimal Average(string Location, string Type, DateTime start, DateTime end);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<DateTime> HighLowByLocation(string Location, string Type, bool Hi, bool Lo, int Min, int Max);
    }

    [DataContract]
    public class Measurement
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
        [DataMember]
        public string StationID { get; set; }
        [DataMember]
        public string LocationName { get; set; }
    }
}
