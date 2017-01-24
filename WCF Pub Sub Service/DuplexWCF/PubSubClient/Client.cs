using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PubSubClient
{
    public class Client
    {
        public delegate void MyEventCallbackHandler(string Id, int Value);
        public static event MyEventCallbackHandler MyEventCallbackEvent;
        private InstanceContext context = null;
        private PubSubServiceReference.PubSubServiceClient client = null;

        [CallbackBehaviorAttribute(UseSynchronizationContext = false)]
        public class ServiceCallback : PubSubServiceReference.IPubSubServiceCallback
        {
            public void ValueChange(string Id, int Value)
            {
                Client.MyEventCallbackEvent(Id, Value);
            }
        }

        public Client()
        {
            InstanceContext context = new InstanceContext(new ServiceCallback());
            PubSubServiceReference.PubSubServiceClient client = new PubSubServiceReference.PubSubServiceClient(context);

            MyEventCallbackHandler callbackHandler = new MyEventCallbackHandler(Print);
            MyEventCallbackEvent += callbackHandler;

            client.Subscribe();
            Console.WriteLine("Client ready...");
        }

        public void Print(string Id, int Value)
        {
            Console.WriteLine("Publisher: {0} Value: {1}", Id, Value);
        }

        public void Close()
        {
            client.Unsubscribe();
        }
    }
}
