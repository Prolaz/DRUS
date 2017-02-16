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
            bool firstPass = true;

            Console.WriteLine("Client connected.");
            string reply = "";

            while (reply!="q")
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
                Console.WriteLine("q.Exit");

                reply = Console.ReadLine();

                switch (reply)
                {
                    case "q":
                        {
                            client.UnsubscribeAll();
                        } break;

                    case "1":
                        {
                            string ID = ChooseStation(ListAllStations(client));
                            Console.WriteLine(client.Subscribe(ID));
                        } break;

                    case "2":
                        {
                            string ID = ChooseStation(ListAllSubscribedStations(client));
                            Console.WriteLine(client.Unsubscribe(ID));
                        } break;

                    case "3":
                        {
                            if (ListAllSubscribedStations(client) != null)
                            {
                                MyEventCallbackEvent += callbackHandler;
                                if (firstPass)
                                {
                                    client.ClientInit();
                                    firstPass = false;
                                }
                                Console.WriteLine("Client ready, starting transmision...");
                            }
                            else Console.WriteLine("You are not subscribed to any station...");
                        } break;
                    case "4":
                        {
                            ListAllSubscribedStations(client);
                        } break;
                    case "5":
                        {
                            string ID = ChooseStation(ListAllSubscribedStations(client));
                            DateTime[] range = GetTimeRange();

                            if (ID == null || range == null) continue;

                            PubSubServiceReference.Measurement[] measurements = client.GetAllMeasurements(ID, range[0], range[1]);

                            foreach (var m in measurements)
                            {
                                Console.WriteLine("{0} {1} {2} {3}", m.StationID, m.Type, m.Value, m.Time);
                            }

                        } break;
                    case "6":
                        {
                            string ID = ChooseStation(ListAllSubscribedStations(client));
                            string type = GetMeasurementType();
                            DateTime[] range = GetTimeRange();

                            if (ID == null || type == null || range == null) continue;

                            PubSubServiceReference.Measurement[] measurements = client.GetSpecificMeasurement(ID, type, range[0], range[1]);

                            foreach (var m in measurements)
                            {
                                Console.WriteLine("{0} {1} {2} {3}", m.StationID, m.Type, m.Value, m.Time);
                            }

                        } break;
                    case "7":
                        {
                            string ID = ChooseStation(ListAllSubscribedStations(client));
                            string type = GetMeasurementType();
                            bool minMax = false;
                            int limit = 0;
                            bool validLimit = GetLimitTypeAndValue(ref minMax, ref limit);

                            if (ID == null || type == null || validLimit == false) continue;

                            DateTime[] retVal = client.GetOutOfRangeMomentsByID(ID, type, minMax, limit);

                            foreach (var dt in retVal)
                            {
                                Console.WriteLine(dt.ToString());
                            }
                        } break;
                    case "8":
                        {
                            string location = GetStationLocation();
                            string type = GetMeasurementType();
                            bool minMax = false;
                            int limit = 0;
                            bool validLimit = GetLimitTypeAndValue(ref minMax, ref limit);

                            if (location == null || type == null || validLimit == false) continue;

                            DateTime[] retVal = client.GetOutOfRangeMomentsByLocation(location, type, minMax, limit);

                            foreach (var dt in retVal)
                            {
                                Console.WriteLine(dt.ToString());
                            }

                        } break;
                    case "9":
                        {
                            string location = GetStationLocation();
                            string type = GetMeasurementType();
                            DateTime[] range = GetTimeRange();

                            if (location == null || type == null || range == null) continue;

                            decimal res = client.GetAverageValue(location, type, range[0], range[1]);
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
            //client.UnsubscribeAll();
        }

        #region Methods

        public void Print(string Id, string Type, int Value)
        {
            Console.WriteLine("Publisher: {0} Type: {1} Value: {2}", Id, Type, Value);
        }

        public string[] ListAllStations(PubSubServiceReference.PubSubServiceClient client)
        {
            Console.WriteLine("List of available stations by ID:");
            string[] publishers = client.ListAllPublishers();

            for (int i = 0; i < publishers.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i, publishers[i]);
            }

            return publishers;
        }

        public string[] ListAllSubscribedStations(PubSubServiceReference.PubSubServiceClient client)
        {
            Console.WriteLine("List of subscribed stations by ID:");
            string[] publishers = client.ListMyPublishers();
            for (int i = 0; i < publishers.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i, publishers[i]);
            }

            return publishers;
        }

        public string ChooseStation(string[] publishers)
        {
            Console.WriteLine("Enter station ID: ");
            string ID = Console.ReadLine();

            if (publishers.Contains(ID)) return ID;

            Console.WriteLine("Entered station ID does not exist.");
            return null;
        }

        public DateTime[] GetTimeRange()
        {
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
                return null;
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
                return null;
            }

            return new DateTime[2] {start, end};
        }

        public string GetMeasurementType()
        {
            Console.WriteLine("Choose measurement type: ");
            Console.WriteLine("1.Temperature[°C]");
            Console.WriteLine("2.Relative Humidity[%]");
            string type = null;
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                type = "Temperature[°C]";
            }
            else if (choice == "2")
            {
                type = "Relative Humidity[%]";
            }
            else Console.WriteLine("Wrong option.");

            return type;
        }

        public bool GetLimitTypeAndValue(ref bool minMax, ref int limit)
        {
            Console.WriteLine("Choose a limit?[min/max]");
            string minMaxString = Console.ReadLine();
            if (minMaxString == "min")
            {
                minMax = false;
            }
            else if (minMaxString == "max")
            {
                minMax = true;
            }
            else
            {
                Console.WriteLine("Wrong option.");
                return false;
            }

            Console.WriteLine("Enter limit value:");
            try
            {
                limit = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Entered value is not a valid number.");
                return false;
            }
            return true;
        }

        public string GetStationLocation()
        {
            List<string> allLocations = (client.ListAllLocations()).ToList();
            List<string> publishers = (client.ListMyPublishers()).ToList();
            List<string> availableLocations = new List<string>();
            foreach (string p in publishers)
            {
                foreach (string l in allLocations)
                {
                    if (p.Contains(l))
                    {
                        availableLocations.Add(l);
                    }
                }
            }
            availableLocations = availableLocations.Distinct().ToList();
            Console.WriteLine("Available locations: ");
            for (int i = 0; i < availableLocations.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i, availableLocations[i]);
            }
            Console.WriteLine("Enter station location: ");
            string location = Console.ReadLine();
            if (availableLocations.Contains(location)) return location;

            Console.WriteLine("Entered location does not exist.");
            return null;
        }

        #endregion
    }
}
