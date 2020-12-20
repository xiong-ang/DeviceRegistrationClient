namespace CFPlusMock
{
    class Session
    {
        public string Token { get; }
        public string  ClientId { get; set; }

        public Session(string token, string clientId)
        {
            Token = token;
            ClientId = clientId;
        }

        public bool IsValid()
        {
            // TODO: Need more validation
            return !string.IsNullOrWhiteSpace(Token) && 
                !string.IsNullOrWhiteSpace(ClientId);
        }
    }
}
