using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace EmbeddedMvc.IdentityServer
{
    public static class Grupy
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
            {
                new Scope
                {
                    Enabled = true,
                    DisplayName = "Grupy",
                    Name = "roles",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                new Scope
                {
                    Enabled = true,
                    DisplayName = "Grupa Główna",
                    Name = "sampleApi",
                    Description = "Dostęp do grupy",
                    Type = ScopeType.Resource,

                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                }
            };
            
            scopes.AddRange(StandardScopes.All);

            return scopes;
        }
    }
}