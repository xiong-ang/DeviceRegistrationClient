using DeviceRegistion;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GrpcServiceMock
{
    public class DeviceRegistionService : DataService.DataServiceBase
    {
        private readonly ILogger<DeviceRegistionService> _logger;
        public DeviceRegistionService(ILogger<DeviceRegistionService> logger)
        {
            _logger = logger;
        }

        public override Task<StatusRespondData> Status(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new StatusRespondData
            {
                IsSucceed = true,
                Status = SeverStatusEnum.Available
            });
        }

        public override Task<AuthRespondData> Auth(AuthRequestData request, ServerCallContext context)
        {
            return Task.FromResult(new AuthRespondData
            {
                IsSucceed = true,
                AuthToken = Guid.NewGuid().ToString()
            });
        }

        public override Task<SaveRespondData> Save(SaveRequestData request, ServerCallContext context)
        {
            Console.WriteLine("Get Data Count: " + request.Devices.Count);
            return Task.FromResult(new SaveRespondData
            {
                IsSucceed = true,
                Message = "Succeed"
            });
        }
    }
}
