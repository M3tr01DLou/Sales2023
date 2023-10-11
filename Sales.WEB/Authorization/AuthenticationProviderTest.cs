using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sales.WEB.Authorization
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimous = new ClaimsIdentity();
            var louUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Lou"),
                new Claim("LastName", "González"),
                new Claim(ClaimTypes.Name, "lou@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));

        }
    }
}
