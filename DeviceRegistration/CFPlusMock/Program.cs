using DeviceRegistion;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CFPlusMock
{
    class Program
    {
        static void Main(string[] args)
        {

            SendDeviceData();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void SendDeviceData()
        {
            var client = new DataService.DataServiceClient(GrpcChannel.ForAddress("https://localhost:5001"));

            Authenticator auth = new Authenticator(client);
            Session session = auth.Authenticate();

            Connector connector = new Connector(client, session);
            List<Task<bool>> sendingTasks = new List<Task<bool>>();
            for (int i = 0; i < 200; i++)
            {
                IEnumerable<Device> devices = MockUtils.Instance.GetMockDevices();
                sendingTasks.Add(connector.SendData(devices));
            }

            Task.WaitAll(sendingTasks.ToArray());
        }


#if false
        static void Test()
        {

            //Create DataService grpc client
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new DataService.DataServiceClient(channel);


            //Call grpc functions
            StatusRespondData statusRespond = client.Status(new Empty());
            if (statusRespond.IsSucceed)
                Console.WriteLine("Status: " + statusRespond.Status);

            AuthRespondData authRespond = client.Auth(new AuthRequestData());
            if (authRespond.IsSucceed)
                Console.WriteLine("Auth Token: " + authRespond.AuthToken);

            SaveRespondData saveRespond = client.Save(new SaveRequestData());
            if (saveRespond.IsSucceed)
                Console.WriteLine("Save Message: " + saveRespond.Message);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
#endif
    }
}
