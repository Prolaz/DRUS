using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Timers;

namespace PublisherClient
{
    public class Publisher
    {
            private Timer t1;
            private Timer t2;
            private Random rnd = new Random();
            private InstanceContext context = null;
            private PubSubServiceReference.PubSubServiceClient client = null;
            public string Name { get; set; }
            public string Location { get; set; }
            public string ID { get; set; }

            [CallbackBehaviorAttribute(UseSynchronizationContext = false)]
            public class ServiceCallback : PubSubServiceReference.IPubSubServiceCallback
            {
                public void ValueChange(string Id, string Type, int Value)
                {
                    Console.WriteLine(Id, Type, Value);
                }
            }

            public Publisher()
            {
                context = new InstanceContext(new ServiceCallback());
                client = new PubSubServiceReference.PubSubServiceClient(context);

                //ID = GenerateID();
                Console.WriteLine("Station name:");
                Name = Console.ReadLine();
                Console.WriteLine("Station location:");
                Location = Console.ReadLine();

                ID = client.PublisherInit(Name, Location);

                t1 = new Timer(1000);
                t1.Elapsed += OnTimer1Elapsed;
                t1.Enabled = true;

                t2 = new Timer(6000);
                t2.Elapsed += OnTimer2Elapsed;
                t2.Enabled = true;

                Console.WriteLine("Publisher ready...");
                Console.WriteLine("Name: {0}  Location: {1}  ID: {2}", Name, Location, ID);
            }

            private void OnTimer1Elapsed(Object sender, ElapsedEventArgs e)
            {
                client.PublishValueChange(ID, "Temperature[°C]", rnd.Next(20, 30));
            }

            private void OnTimer2Elapsed(Object sender, ElapsedEventArgs e)
            {
                client.PublishValueChange(ID, "Relative Humidity[%]", rnd.Next(25, 60));
            }
    }
}
