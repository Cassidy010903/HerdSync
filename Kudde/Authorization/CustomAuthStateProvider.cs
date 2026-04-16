using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;

namespace Kudde.Authorization
{
    public class CustomAuthStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider(
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor) : base(loggerFactory)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState,
            CancellationToken cancellationToken)
        {
            // You can add token/session invalidation checks here later
            return Task.FromResult(true);
        }

        public async Task LoginAsync(HttpContext httpContext, ClaimsPrincipal user)
        {
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                user,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}