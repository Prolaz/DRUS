using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SubscriberClient
{
    class Subscriber
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
                if (MyEventCallbackEvent != null)
                {
                    MyEventCallbackEvent(Id, Type, Value);
                }
            }
        }

        public Subscriber()
        {
            context = new InstanceContext(new ServiceCallback());
            client = new PubSubServiceReference.PubSubServiceClient(context);
            MyEventCallbackHandler callbackHandler = new MyEventCallbackHandler(Print);
            bool exit = false;
            bool first = true;

            Console.WriteLine("Client connected.");

            while (!exit)
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1.Subscribe");
                Console.WriteLine("2.Unsubscribe");
                Console.WriteLine("3.View live measurements");
                Console.WriteLine("4.View publishers list");
                Console.WriteLine("5.Collect all measurements from a station");
                Console.WriteLine("6.Collect specific measurement type from a station");
                Console.WriteLine("7.Collect all the moments when a specific measurement was out of range for a specific station");
                Console.WriteLine("8.Collect all the moments when a specific measurement was out of range for a specific location");
                Console.WriteLine("9.Average temperature/humidity value for a specific location");
                Console.WriteLine("0.Exit");

                string reply = Console.ReadLine();
                int replyNum = 0;

                try
                {
                    replyNum = Convert.ToInt16(reply);
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (replyNum)
                {
                    case 0:
                        {
                            client.UnsubscribeAll();
                            exit = true;
                        } break;

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
                        } break;

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
                        } break;

                    case 3:
                        {
                            MyEventCallbackEvent += callbackHandler;
                            if (first)
                            {
                                client.ClientInit();
                                first = false;
                            }
                            Console.WriteLine("Client ready, starting transmision..."); 
                        } break;
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

                            DateTime start, end;

                            Console.WriteLine("Enter starting date and time: [yyyy-MM-dd HH:mm:ss] ");
                            string dateTime = Console.ReadLine();

                            try
                            {
                                start = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null);
                            }
                            catch
                            {
                                Console.WriteLine("Wrong DateTime form.");
                                continue;
                            }

                            Console.WriteLine("Enter ending date and time: [yyyy-MM-dd HH:mm:ss] ");
                            dateTime = Console.ReadLine();
                            try
                            {
                                end = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null);
                            }
                            catch
                            {
                                Console.WriteLine("Wrong DateTime form.");
                                continue;
                            }

                            PubSubServiceReference.Measurement[] measurements = client.AllMeasurementsFromTo(ID, start, end);

                            foreach (var m in measurements)
                            {
                                Console.WriteLine("{0} {1} {2} {3}", m.StationID, m.Type, m.Value, m.Time);
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
                            Console.WriteLine("2.Relative Humidity[%]");
                            string type = "";
                            int choice = Convert.ToInt32(Console.ReadLine());
                            if (choice == 1)
                            {
                                type = "Temperature[°C]";
                            }
                            else
                            {
                                type = "Relative Humidity[%]";
                            }

                            DateTime start, end;

                            Console.WriteLine("Enter starting date and time: [yyyy-MM-dd HH:mm:ss] ");
                            string dateTime = Console.ReadLine();

                            try
                            {
                                start = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null);
                            }
                            catch
                            {
                                Console.WriteLine("Wrong DateTime form.");
                                continue;
                            }

                            Console.WriteLine("Enter ending date and time: [yyyy-MM-dd HH:mm:ss] ");
                            dateTime = Console.ReadLine();
                            try
                            {
                                end = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null);
                            }
                            catch
                            {
                                Console.WriteLine("Wrong DateTime form.");
                                continue;
                            }

                            PubSubServiceReference.Measurement[] measurements = client.CertainMeasurementFromTo(ID, type, start, end);

                            foreach (var m in measurements)
                            {
                                Console.WriteLine("{0} {1} {2} {3}", m.StationID, m.Type, m.Value, m.Time);
                            }

                        } break;
                    case 7:
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
                            Console.WriteLine("2.Relative Humidity[%]");
                            string type = "";
                            int choice = Convert.ToInt32(Console.ReadLine());
                            if (choice == 1)
                            {
                                type = "Temperature[°C]";
                            }
                            else
                            {
                                type = "Relative Humidity[%]";
                            }
                            Console.WriteLine("Choose a limit?[min/max]");
                            string minMax = Console.ReadLine();
                            bool Lo = false;
                            bool Hi = false;
                            if (minMax.Equals("min"))
                            {
                                Lo = true;
                            }
                            else
                            {
                                Hi = true;
                            }
                            Console.WriteLine("Enter limit value:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            DateTime[] retVal = client.HighLowByID(ID, type, Hi, Lo, limit, limit);

                            foreach (var dt in retVal)
                            {
                                Console.WriteLine(dt.ToString());
                            }
                        } break;
                    case 8:
                        {
                            Console.WriteLine("Enter station location: ");
                            string location = Console.ReadLine();
                            Console.WriteLine("Choose measurement type: ");
                            Console.WriteLine("1.Temperature[°C]");
                            Console.WriteLine("2.Relative Humidity[%]");
                            string type = "";
                            int choice = Convert.ToInt32(Console.ReadLine());
                            if (choice == 1)
                            {
                                type = "Temperature[°C]";
                            }
                            else
                            {
                                type = "Relative Humidity[%]";
                            }
                            Console.WriteLine("Choose a limit type?[min/max]");
                            string minMax = Console.ReadLine();
                            bool Lo = false;
                            bool Hi = false;
                            if (minMax.Equals("min"))
                            {
                                Lo = true;
                            }
                            else
                            {
                                Hi = true;
                            }
                            Console.WriteLine("Enter limit value:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            DateTime[] retVal = client.HighLowByLocation(location, type, Hi, Lo, limit, limit);

                            foreach (var dt in retVal)
                            {
                                Console.WriteLine(dt.ToString());
                            }

                        } break;
                    case 9:
                        {
                            Console.WriteLine("Enter station location: ");
                            string location = Console.ReadLine();
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

                            DateTime start, end;

                            Console.WriteLine("Enter starting date and time: [yyyy-MM-dd HH:mm:ss] ");
                            string dateTime = Console.ReadLine();

                            try
                            {
                                start = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null);
                            }
                            catch
                            {
                                Console.WriteLine("Wrong DateTime form.");
                                continue;
                            }

                            Console.WriteLine("Enter ending date and time: [yyyy-MM-dd HH:mm:ss] ");
                            dateTime = Console.ReadLine();
                            try
                            {
                                end = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null);
                            }
                            catch
                            {
                                Console.WriteLine("Wrong DateTime form.");
                                continue;
                            }

                            decimal res = client.Average(location, type, start, end);
                            Console.WriteLine("Average value of {0} for location {1} is: {2}", type, location, res);
                        } break;

                    default:
                        {
                            Console.WriteLine("Wrong option.");
                        } break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                if (MyEventCallbackEvent != null)
                {
                    MyEventCallbackEvent -= callbackHandler;
                }
            }
            client.UnsubscribeAll();
        }

        public void Print(string Id, string Type, int Value)
        {
            Console.WriteLine("Publisher: {0} Type: {1} Value: {2}", Id, Type, Value);
        }
    }
}
