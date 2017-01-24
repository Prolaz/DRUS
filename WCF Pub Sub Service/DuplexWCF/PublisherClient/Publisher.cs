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
            private Timer t;
            private Random rnd = new Random();
            private InstanceContext context = null;
            private PubSubServiceReference.PubSubServiceClient client = null;
            public string Ime { get; set; }
            public string Lokacija { get; set; }
            public string ID { get; set; }

            [CallbackBehaviorAttribute(UseSynchronizationContext = false)]
            public class ServiceCallback : PubSubServiceReference.IPubSubServiceCallback
            {
                public void ValueChange(string Id, int Value)
                {
                    Console.WriteLine(Id, Value);
                }
            }

            public Publisher()
            {
                context = new InstanceContext(new ServiceCallback());
                client = new PubSubServiceReference.PubSubServiceClient(context);

                //ID = GenerateID();
                Console.WriteLine("Unesite ime meraca:");
                Ime = Console.ReadLine();
                Console.WriteLine("Unesite lokaciju meraca:");
                Lokacija = Console.ReadLine();

                ID = client.PublisherInit(Ime, Lokacija);

                t = new Timer(1000);
                t.Elapsed += OnTimerElapsed;
                t.Enabled = true;

                Console.WriteLine("Publisher ready...");
                Console.WriteLine("Ime: {0}  Lokacija: {1} ", Ime, Lokacija);
            }

            private void OnTimerElapsed(Object sender, ElapsedEventArgs e)
            {
                client.PublishValueChange(ID, rnd.Next(0, 1000));
            }
    }
}
