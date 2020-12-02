using DeviceRegistion;
using Grpc.Net.Client;
using System;

namespace GrpcComms
{

    public class DeviceRegistionService
    {
        public static void Test()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new DataService.DataServiceClient(channel);
            var reply = client.Save(new DataRequest { Name="hahhahha"});
            Console.WriteLine("Save: " + reply.Message);
            reply = client.Read(new DataRequest { Name = "hahhahha" });
            Console.WriteLine("Read: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
