using DeviceRegistion;
using Grpc.Net.Client;
using System;

namespace CFPlusMock
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create DataService grpc client
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new DataService.DataServiceClient(channel);


            //Call grpc functions
            StatusRespondData statusRespond = client.Status(new Empty());
            if(statusRespond.IsSucceed)
                Console.WriteLine("Status: " + statusRespond.Status);

            AuthRespondData authRespond = client.Auth(new AuthRequestData());
            if(authRespond.IsSucceed)
                Console.WriteLine("Auth Token: " + authRespond.AuthToken);

            SaveRespondData saveRespond = client.Save(new SaveRequestData());
            if(saveRespond.IsSucceed)
                Console.WriteLine("Save Message: " + saveRespond.Message);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
