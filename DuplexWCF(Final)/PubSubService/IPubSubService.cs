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
        [OperationContract]
        string PublisherInit(string Name, string Location);
        [OperationContract]
        void ClientInit();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        string Subscribe(string ID);
        [OperationContract]
        string Unsubscribe(string ID);
        [OperationContract]
        void UnsubscribeAll();
        [OperationContract]
        void PublishValueChange(string Id, string Type, int Value);
        [OperationContract]
        List<string> ListAllPublishers();
        [OperationContract]
        List<string> ListMyPublishers();
        [OperationContract]
        List<string> ListAllLocations();
        [OperationContract]
        List<Measurement> GetAllMeasurements(string ID, DateTime start, DateTime end);
        [OperationContract]
        List<Measurement> GetSpecificMeasurement(string ID, string Type, DateTime start, DateTime end);
        [OperationContract]
        decimal GetAverageValue(string Location, string Type, DateTime start, DateTime end);
        [OperationContract]
        DateTime[] GetOutOfRangeMomentsByID(string ID, string Type, bool HiLo, int Limit);
        [OperationContract]
        DateTime[] GetOutOfRangeMomentsByLocation(string Location, string Type, bool HiLo, int Limit);
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
