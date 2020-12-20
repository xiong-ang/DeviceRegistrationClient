using DeviceRegistion;

namespace CFPlusMock
{
    class Authenticator
    {
        #region authentication constants
        public static readonly string clientId = "mockClientId";
        public static readonly string clientSecret = "mockClientSecret";
        #endregion


        #region private methods
        private DataService.DataServiceClient grpcClient;
        #endregion


        public Authenticator(DataService.DataServiceClient client)
        {
            grpcClient = client;
        }

        public Session Authenticate()
        {
            if (!IsServerAviable())
                return null;

            string token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
                return null;

            return new Session(token, clientId);
        }


        #region private methods
        private bool IsServerAviable()
        {
            StatusRespondData statusRespond = grpcClient.Status(new Empty());
            return statusRespond.IsSucceed;
        }

        private string GetToken()
        {
            AuthRespondData authRespond = grpcClient.Auth(new AuthRequestData() { 
                ClientId = clientId,
                ClientSecret = clientSecret
            });
            if (authRespond.IsSucceed)
                return authRespond.AuthToken;

            return string.Empty;
        }
        #endregion
    }
}
