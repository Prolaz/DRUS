using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PubSubClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Console.ReadKey();
            client.Close();
        }
    }
}
