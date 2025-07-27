namespace ADUSAPI.Services
{
    public class RefreshTokenStore
    {
        private readonly Dictionary<string, string> _store = new();

        public void SaveToken(string username, string refreshToken)
        {
            _store[username] = refreshToken;
        }

        public string GetToken(string username)
        {
            return _store.ContainsKey(username) ? _store[username] : null;
        }

        public bool ValidateToken(string username, string refreshToken)
        {
            return _store.ContainsKey(username) && _store[username] == refreshToken;
        }
    }
}