﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PubSubClient.PubSubServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ListPublishSubscribe.Service", ConfigurationName="PubSubServiceReference.IPubSubService", CallbackContract=typeof(PubSubClient.PubSubServiceReference.IPubSubServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IPubSubService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/Subscribe", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/SubscribeResponse")]
        string Subscribe(string ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/Subscribe", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/SubscribeResponse")]
        System.Threading.Tasks.Task<string> SubscribeAsync(string ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/Unsubscribe", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/UnsubscribeResponse")]
        string Unsubscribe(string ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/Unsubscribe", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/UnsubscribeResponse")]
        System.Threading.Tasks.Task<string> UnsubscribeAsync(string ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/UnsubscribeAll", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/UnsubscribeAllResponse")]
        void UnsubscribeAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/UnsubscribeAll", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/UnsubscribeAllResponse")]
        System.Threading.Tasks.Task UnsubscribeAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/ListAllPublishers", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/ListAllPublishersResponse")]
        string[] ListAllPublishers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/ListAllPublishers", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/ListAllPublishersResponse")]
        System.Threading.Tasks.Task<string[]> ListAllPublishersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/PublisherInit", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/PublisherInitResponse")]
        string PublisherInit(string Ime, string Lokacija);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/PublisherInit", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/PublisherInitResponse")]
        System.Threading.Tasks.Task<string> PublisherInitAsync(string Ime, string Lokacija);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/ClientInit", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/ClientInitResponse")]
        void ClientInit();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/ClientInit", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/ClientInitResponse")]
        System.Threading.Tasks.Task ClientInitAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/PublishValueChange", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/PublishValueChangeResponse")]
        void PublishValueChange(string Id, string Type, int Value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ListPublishSubscribe.Service/IPubSubService/PublishValueChange", ReplyAction="http://ListPublishSubscribe.Service/IPubSubService/PublishValueChangeResponse")]
        System.Threading.Tasks.Task PublishValueChangeAsync(string Id, string Type, int Value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPubSubServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://ListPublishSubscribe.Service/IPubSubService/ValueChange")]
        void ValueChange(string Id, string Type, int Value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPubSubServiceChannel : PubSubClient.PubSubServiceReference.IPubSubService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PubSubServiceClient : System.ServiceModel.DuplexClientBase<PubSubClient.PubSubServiceReference.IPubSubService>, PubSubClient.PubSubServiceReference.IPubSubService {
        
        public PubSubServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public PubSubServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public PubSubServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PubSubServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PubSubServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string Subscribe(string ID) {
            return base.Channel.Subscribe(ID);
        }
        
        public System.Threading.Tasks.Task<string> SubscribeAsync(string ID) {
            return base.Channel.SubscribeAsync(ID);
        }
        
        public string Unsubscribe(string ID) {
            return base.Channel.Unsubscribe(ID);
        }
        
        public System.Threading.Tasks.Task<string> UnsubscribeAsync(string ID) {
            return base.Channel.UnsubscribeAsync(ID);
        }
        
        public void UnsubscribeAll() {
            base.Channel.UnsubscribeAll();
        }
        
        public System.Threading.Tasks.Task UnsubscribeAllAsync() {
            return base.Channel.UnsubscribeAllAsync();
        }
        
        public string[] ListAllPublishers() {
            return base.Channel.ListAllPublishers();
        }
        
        public System.Threading.Tasks.Task<string[]> ListAllPublishersAsync() {
            return base.Channel.ListAllPublishersAsync();
        }
        
        public string PublisherInit(string Ime, string Lokacija) {
            return base.Channel.PublisherInit(Ime, Lokacija);
        }
        
        public System.Threading.Tasks.Task<string> PublisherInitAsync(string Ime, string Lokacija) {
            return base.Channel.PublisherInitAsync(Ime, Lokacija);
        }
        
        public void ClientInit() {
            base.Channel.ClientInit();
        }
        
        public System.Threading.Tasks.Task ClientInitAsync() {
            return base.Channel.ClientInitAsync();
        }
        
        public void PublishValueChange(string Id, string Type, int Value) {
            base.Channel.PublishValueChange(Id, Type, Value);
        }
        
        public System.Threading.Tasks.Task PublishValueChangeAsync(string Id, string Type, int Value) {
            return base.Channel.PublishValueChangeAsync(Id, Type, Value);
        }
    }
}
