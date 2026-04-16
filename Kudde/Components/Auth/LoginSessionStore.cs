using System.Security.Claims;

namespace Kudde.Components.Auth
{
    public class LoginSessionStore
    {
        private readonly Dictionary<string, ClaimsPrincipal> _pending = new();

        public string Store(ClaimsPrincipal principal)
        {
            var token = Guid.NewGuid().ToString("N");
            _pending[token] = principal;
            return token;
        }

        public ClaimsPrincipal Retrieve(string token)
        {
            _pending.TryGetValue(token, out var principal);
            _pending.Remove(token);
            return principal;
        }
    }
}
