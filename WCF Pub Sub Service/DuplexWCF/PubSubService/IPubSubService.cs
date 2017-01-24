using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PubSubService
{
    [ServiceContract(Namespace = "http://ListPublishSubscribe.Service",
    SessionMode = SessionMode.Required, CallbackContract = typeof(IPubSubContract))]
    public interface IPubSubService
    {
        [OperationContract(IsOneWay = false, IsInitiating=true)]
        string Subscribe(string ID);
        [OperationContract(IsOneWay = false, IsInitiating=true)]
        string Unsubscribe(string ID);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void UnsubscribeAll();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<string> ListAllPublishers();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        string PublisherInit(string Ime, string Lokacija);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void ClientInit();
        [OperationContract(IsOneWay = false)]
        void PublishValueChange(string Id, int Value);
    }
}
