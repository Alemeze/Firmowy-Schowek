using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace EmbeddedMvc.IdentityServer
{
    public static class Uzytkownik
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "uzytkownik",
                    Password = "haslo",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Użytkownik"),
                        new Claim(Constants.ClaimTypes.FamilyName, "WSB"),
                        new Claim(Constants.ClaimTypes.Role, "Grupa 1"),
                        new Claim(Constants.ClaimTypes.Role, "Grupa 2")
                    }
                }
            };
        }
    }
}