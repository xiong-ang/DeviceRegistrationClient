using DeviceRegistion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CFPlusMock
{
    class Connector
    {
        #region private methods
        private DataService.DataServiceClient grpcClient;
        private Session grpcSession;
        #endregion


        public Connector(DataService.DataServiceClient client, Session session)
        {
            grpcClient = client;
            grpcSession = session;
        }

        public async Task<bool> SendData(IEnumerable<Device> deviceList)
        {
            if (!grpcSession.IsValid())
                return false;

            var requestData = new SaveRequestData()
            {
                AuthToken = grpcSession.Token,
                ClientId = grpcSession.ClientId,
            };
            requestData.Devices.AddRange(deviceList);

            SaveRespondData saveRespond =  await grpcClient.SaveAsync(requestData);

            if (!saveRespond.IsSucceed)
                Console.WriteLine("Save Message: " + saveRespond.Message);

            return true;
        }
    }
}
