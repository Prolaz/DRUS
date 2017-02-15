using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PubSubService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PubSubService : IPubSubService
    {
        public delegate void ValueChangeEventHandler(object sender, ServiceEventArgs e);
        public static event ValueChangeEventHandler ValueChangeEvent;
        ProjectDatabaseEntities entities = new ProjectDatabaseEntities();
        private List<string> listeningTo = new List<string>();
        IPubSubContract ServiceCallback = null;
        ValueChangeEventHandler ValueHandler = null;

        public List<string> ListAllPublishers()
        {
            List<string> publishers = new List<string>();
            foreach (var s in entities.STATIONS)
            {
                publishers.Add(s.ID);
            }

            return publishers;
        }

        public List<string> ListMyPublishers()
        {
            return listeningTo;
        }

        public List<string> ListAllLocations()
        {
            List<string> loc = new List<string>();
            foreach (var e in entities.LOCATIONS)
            {
                loc.Add(e.NAME);
            }

            return loc;
        }

        public string Subscribe(string ID)
        {
            var station = (from s in entities.STATIONS
                           where s.ID == ID
                           select s).FirstOrDefault();
            if (station != null)
            {
                if (!listeningTo.Contains(ID))
                {
                    listeningTo.Add(ID);
                    return "Subscription to station " + ID + " successful";
                }
                return "You are already subscribed to station " + ID;
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
            if (listeningTo.Count != 0)
            {
                listeningTo = new List<string>();
            }
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
            string ID = Name + Location.Replace(" ", String.Empty);

            var station = (from s in entities.STATIONS
                           where s.ID == ID
                           select s).FirstOrDefault();
            if (station == null)
            {
                station = new STATION();
                station.ID = ID;
                station.NAME = Name;
                station.LOCATION_ID = (from l in entities.LOCATIONS
                                       where l.NAME == Location
                                       select l.ID).FirstOrDefault();
                entities.STATIONS.AddObject(station);
                entities.SaveChanges();
            }

            return ID;
        }

        public List<Measurement> AllMeasurementsFromTo(string ID, DateTime start, DateTime end)
        {
            List<Measurement> retVal = new List<Measurement>();

            var measurements = (from m in entities.MEASUREMENTS
                                where m.TIME >= start && m.TIME <= end && m.STATION_ID == ID
                                select m).ToArray();

            foreach (var m in measurements)
            {
                Measurement temp = new Measurement();
                temp.ID = m.ID;
                temp.Value = m.VALUE;
                temp.Type = m.TYPE;
                temp.Time = m.TIME;
                temp.StationID = m.STATION_ID;
                temp.LocationName = m.STATION.LOCATION.NAME;
                retVal.Add(temp);
            }

            return retVal;
        }

        public List<Measurement> CertainMeasurementFromTo(string ID, string Type, DateTime start, DateTime end)
        {
            List<Measurement> retVal = new List<Measurement>();

            var measurements = (from m in entities.MEASUREMENTS
                                where m.TIME >= start && m.TIME <= end && m.STATION_ID == ID && m.TYPE == Type
                                select m).ToArray();


            foreach (var m in measurements)
            {
                Measurement temp = new Measurement();
                temp.ID = m.ID;
                temp.Value = m.VALUE;
                temp.Type = m.TYPE;
                temp.Time = m.TIME;
                temp.StationID = m.STATION_ID;
                temp.LocationName = m.STATION.LOCATION.NAME;
                retVal.Add(temp);
            }

            return retVal;
        }

        public List<DateTime> HighLowByID(string ID, string Type, bool Hi, bool Lo, int Min, int Max)
        {
            List<DateTime> retVal = new List<DateTime>();

            if (Hi)
            {
                var measurements = (from m in entities.MEASUREMENTS
                                    where m.STATION_ID == ID && m.TYPE == Type && m.VALUE >= Max
                                    select m.TIME).ToArray();
                foreach (var m in measurements)
                {
                    retVal.Add(m);
                }

                return retVal;

            }
            else if (Lo)
            {
                var measurements = (from m in entities.MEASUREMENTS
                                    where m.STATION_ID == ID && m.TYPE == Type && m.VALUE <= Min
                                    select m.TIME).ToArray();
                foreach (var m in measurements)
                {
                    retVal.Add(m);
                }

                return retVal;
            }


            return retVal;
        }

        public decimal Average(string Location, string Type, DateTime start, DateTime end)
        {
            var measurements = (from m in entities.MEASUREMENTS
                                where m.TIME >= start && m.TIME <= end && m.STATION.LOCATION.NAME == Location && m.TYPE == Type
                                select m.VALUE).ToArray();

            decimal sum = measurements.Sum();
            decimal retVal = sum / measurements.Length;

            return retVal;
        }

        public List<DateTime> HighLowByLocation(string Location, string Type, bool Hi, bool Lo, int Min, int Max)
        {
            List<DateTime> retVal = new List<DateTime>();

            if (Hi)
            {
                var measurements = (from m in entities.MEASUREMENTS
                                    where m.STATION.LOCATION.NAME == Location && m.TYPE == Type && m.VALUE >= Max
                                    select m.TIME).ToArray();
                foreach (var m in measurements)
                {
                    retVal.Add(m);
                }

                return retVal;

            }
            else if (Lo)
            {
                var measurements = (from m in entities.MEASUREMENTS
                                    where m.STATION.LOCATION.NAME == Location && m.TYPE == Type && m.VALUE <= Min
                                    select m.TIME);
                foreach (var m in measurements)
                {
                    retVal.Add(m);
                }

                return retVal;
            }


            return retVal;
        }

        public void PublishValueChange(string Id, string Type, int Value)
        {
            ServiceEventArgs se = new ServiceEventArgs();
            se.Id = Id;
            se.Type = Type;
            se.Value = Value;
            MEASUREMENT m = new MEASUREMENT();
            m.VALUE = Value;
            m.TYPE = Type;
            m.STATION_ID = (from s in entities.STATIONS
                            where s.ID == Id
                            select s.ID).FirstOrDefault();
            m.TIME = DateTime.Now;
            entities.MEASUREMENTS.AddObject(m);
            entities.SaveChanges();

            if (ValueChangeEvent != null)
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
