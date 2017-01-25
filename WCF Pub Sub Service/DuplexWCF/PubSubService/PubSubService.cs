using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PubSubService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PubSubService : IPubSubService
    {
        public delegate void ValueChangeEventHandler(object sender, ServiceEventArgs e);
        public static event ValueChangeEventHandler ValueChangeEvent;
        private static readonly List<string> _publishers = new List<string>();
        private List<string> listeningTo = new List<string>();
        //private Dictionary<string, int> measurements = new Dictionary<string, int>();
        //public Dictionary<string, int> Measurements
        //{
        //    get
        //    {
        //        return measurements;
        //    }
        //    set
        //    {
        //        measurements = value;
        //    }
        //}

        IPubSubContract ServiceCallback = null;
        ValueChangeEventHandler ValueHandler = null;

        public List<string> ListAllPublishers()
        {
            lock (_publishers)
            {
                return _publishers;
            }
        }

        public string Subscribe(string ID)
        {
            lock (_publishers)
            {
                if (_publishers.Contains(ID))
                {
                    if (!listeningTo.Contains(ID))
                    {
                        listeningTo.Add(ID);
                        return "Subscription to station " + ID + " successful";
                    }
                    return "You are already subscribed to station " + ID;
                }
            }
            return "Subscription failed. There is no station with selected ID: " + ID;
        }

        public string Unsubscribe(string ID)
        {
            if (listeningTo.Contains(ID))
            {
                listeningTo.Remove(ID);
                return "Unsubscription from station " + ID + " successful";
            }
            return "Unsubscription failed. You are not subscribed to station with selected ID: " + ID;
        }

        public void UnsubscribeAll()
        {
            ValueChangeEvent -= ValueHandler;
        }

        public void ClientInit()
        {
            ServiceCallback = OperationContext.Current.GetCallbackChannel<IPubSubContract>();
            ValueHandler = new ValueChangeEventHandler(PublishValueChangeHandler);
            ValueChangeEvent += ValueHandler;
        }

        public string PublisherInit(string Name, string Location)
        {
            string ID = Name + Location;

            lock (_publishers)
            {
                if (!_publishers.Contains(ID))
                {
                    _publishers.Add(ID);
                }
            }

            return ID;
        }

        public void PublishValueChange(string Id, string Type, int Value)
        {
            ServiceEventArgs se = new ServiceEventArgs();
            se.Id = Id;
            se.Type = Type;
            se.Value = Value;
            //Measurements[Id] = Value;

            if (ValueChangeEvent!=null)
                ValueChangeEvent(this, se);
        }

        public void PublishValueChangeHandler(object sender, ServiceEventArgs se)
        {
            if (listeningTo.Contains(se.Id))
            {
                ServiceCallback.ValueChange(se.Id, se.Type, se.Value);
            }
        }

        public class ServiceEventArgs : EventArgs
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public int Value { get; set; }
        }
    }
}
