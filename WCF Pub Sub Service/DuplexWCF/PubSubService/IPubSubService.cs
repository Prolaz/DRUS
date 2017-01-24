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
        void Subscribe();
        [OperationContract(IsOneWay = false, IsInitiating=true)]
        void Unsubscribe();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        string PublisherInit(string Ime, string Lokacija);
        [OperationContract(IsOneWay = false)]
        void PublishValueChange(string Id, int Value);
    }
}
