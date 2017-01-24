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

        public void Subscribe()
        {
            ServiceCallback = OperationContext.Current.GetCallbackChannel<IPubSubContract>();
            ValueHandler = new ValueChangeEventHandler(PublishValueChangeHandler);
            ValueChangeEvent += ValueHandler;
        }

        public void Unsubscribe()
        {
            ValueChangeEvent -= ValueHandler;
        }

        public string PublisherInit(string Ime, string Lokacija)
        {
            string ID = Ime + Lokacija;

            lock (_publishers)
            {
                if (!_publishers.Contains(ID))
                {
                    _publishers.Add(ID);
                }
            }

            return ID;
        }

        public void PublishValueChange(string Id, int Value)
        {
            ServiceEventArgs se = new ServiceEventArgs();
            se.Id = Id;
            se.Value = Value;
            //Measurements[Id] = Value;

            if (ValueChangeEvent!=null)
                ValueChangeEvent(this, se);
        }

        public void PublishValueChangeHandler(object sender, ServiceEventArgs se)
        {
            ServiceCallback.ValueChange(se.Id, se.Value);

        }

        public class ServiceEventArgs : EventArgs
        {
            public string Id { get; set; }
            public int Value { get; set; }
        }
    }
}
