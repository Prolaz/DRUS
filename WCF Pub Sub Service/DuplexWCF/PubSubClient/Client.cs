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
        public delegate void MyEventCallbackHandler(string Id, string Type, int Value);
        public static event MyEventCallbackHandler MyEventCallbackEvent;
        private InstanceContext context = null;
        private PubSubServiceReference.PubSubServiceClient client = null;

        [CallbackBehaviorAttribute(UseSynchronizationContext = false)]
        public class ServiceCallback : PubSubServiceReference.IPubSubServiceCallback
        {
            public void ValueChange(string Id, string Type, int Value)
            {
                Client.MyEventCallbackEvent(Id, Type, Value);
            }
        }

        public Client()
        {
            context = new InstanceContext(new ServiceCallback());
            client = new PubSubServiceReference.PubSubServiceClient(context);
            MyEventCallbackHandler callbackHandler = new MyEventCallbackHandler(Print);

            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1.Subscribe");
            Console.WriteLine("2.Unsubscribe");
            Console.WriteLine("3.View live measurements");
            Console.WriteLine("4.View publishers list");
            Console.WriteLine("5.Collect all measurements from a station");
            Console.WriteLine("6.Collect specific measurement type from a station");
            Console.WriteLine("7.Collect all the moments when a specific measurement was out of range for a specific station");
            Console.WriteLine("8.Collect all the moments when a specific measurement was out of range for a specific location");
            Console.WriteLine("9.Average temperature/humidity value for a station");
            Console.WriteLine("0.Exit");

            string reply = Console.ReadLine();

            int replyNum = Convert.ToInt16(reply);

            switch (replyNum)
            {
                case 0:
                    {
                        client.UnsubscribeAll();
                    }break;

                case 1:
                    {
                        Console.WriteLine("List of available stations by ID:");
                        string[] publishers = client.ListAllPublishers();

                        for (int i = 0; i < publishers.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}", i, publishers[i]);
                        }

                        Console.WriteLine("Enter station ID: ");
                        string ID = Console.ReadLine();
                        string msg = client.Subscribe(ID);
                        Console.WriteLine(msg);
                    }break;

                case 2:
                    {
                        Console.WriteLine("List of subscribed stations by ID:");
                        string[] publishers = client.ListMyPublishers();
                        for (int i = 0; i < publishers.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}", i, publishers[i]);
                        }

                        Console.WriteLine("Enter station ID: ");
                        string ID = Console.ReadLine();
                        string msg = client.Unsubscribe(ID);
                        Console.WriteLine(msg);
                    }break;

                case 3:
                    {
                        MyEventCallbackEvent += callbackHandler;
                    }break;
                case 4:
                    {
                        Console.WriteLine("List of subscribed stations by ID:");
                        string[] publishers = client.ListMyPublishers();
                        for (int i = 0; i < publishers.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}", i, publishers[i]);
                        }
                    } break;
                case 5:
                    {
                        Console.WriteLine("List of subscribed stations by ID:");
                        string[] publishers = client.ListMyPublishers();
                        for (int i = 0; i < publishers.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}", i, publishers[i]);
                        }

                        Console.WriteLine("Enter station ID: ");
                        string ID = Console.ReadLine();
                        Console.WriteLine("Start time:");
                        Console.WriteLine("Enter year: ");
                        int year = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter month: ");
                        int month = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter day: ");
                        int day = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter hour: ");
                        int hour = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter minute: ");
                        int minute = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter second: ");
                        int second = Convert.ToInt32(Console.ReadLine());
                        DateTime start = new DateTime(year, month, day, hour, minute, second);
                        Console.WriteLine("End time:");
                        Console.WriteLine("Enter year: ");
                        year = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter month: ");
                        month = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter day: ");
                        day = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter hour: ");
                        hour = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter minute: ");
                        minute = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter second: ");
                        second = Convert.ToInt32(Console.ReadLine());
                        DateTime end = new DateTime(year, month, day, hour, minute, second);

                        PubSubServiceReference.Measurement[] measurements = client.AllMeasurementsFromTo(ID, start, end);

                        foreach (var m in measurements)
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", m.StationID, m.Type, m.Value, m.Time);
                        }

                    } break;
                case 6:
                    {
                        Console.WriteLine("List of subscribed stations by ID:");
                        string[] publishers = client.ListMyPublishers();
                        for (int i = 0; i < publishers.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}", i, publishers[i]);
                        }

                        Console.WriteLine("Enter station ID: ");
                        string ID = Console.ReadLine();
                        Console.WriteLine("Choose measurement type: ");
                        Console.WriteLine("1.Temperature[°C]");
                        Console.WriteLine("2.RelativeHumidity[%]");
                        string type = "";
                        int choice = Convert.ToInt32(Console.ReadLine());
                        if (choice == 1)
                        {
                            type = "Temperature[°C]";
                        }
                        else
                        {
                            type = "RelativeHumidity[%]";
                        }
                        Console.WriteLine("Start time:");
                        Console.WriteLine("Enter year: ");
                        int year = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter month: ");
                        int month = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter day: ");
                        int day = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter hour: ");
                        int hour = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter minute: ");
                        int minute = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter second: ");
                        int second = Convert.ToInt32(Console.ReadLine());
                        DateTime start = new DateTime(year, month, day, hour, minute, second);
                        Console.WriteLine("End time:");
                        Console.WriteLine("Enter year: ");
                        year = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter month: ");
                        month = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter day: ");
                        day = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter hour: ");
                        hour = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter minute: ");
                        minute = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter second: ");
                        second = Convert.ToInt32(Console.ReadLine());
                        DateTime end = new DateTime(year, month, day, hour, minute, second);

                        PubSubServiceReference.Measurement[] measurements = client.CertainMeasurementFromTo(ID, type, start, end);

                        foreach (var m in measurements)
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", m.StationID, m.Type, m.Value, m.Time);
                        }

                    } break;
                case 7:
                    {

                    } break;
                case 8:
                    {

                    } break;
                case 9:
                    {

                    } break;
            }

           // string[] publishers = client.ListAllPublishers();


            client.ClientInit();
            Console.WriteLine("Client ready, starting transmision...");          
        }

        public void Print(string Id, string Type, int Value)
        {
            Console.WriteLine("Publisher: {0} Type: {1} Value: {2}", Id, Type, Value);
        }

        public void Close()
        {
            client.UnsubscribeAll();
        }
    }
}
