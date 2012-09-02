using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Redis.Redis;
using WCFChannelTest2;

namespace Redis
{
    class Program
    {
        private static ServiceHost host;

        static void Main(string[] args)
        {
            host = new ServiceHost(typeof(Service));
            host.Description.Endpoints[0].Behaviors.Add(new MessageInspector());

            host.Open();

            Console.WriteLine("Service is ready.");
        }
    }
}
