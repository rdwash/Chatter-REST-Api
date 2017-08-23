using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace ChatterApi.IdSvr.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                new Scope
                {
                    Enabled = true,
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "The roles you belong to.",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                new Scope {
                    Name = "chatterapi",
                    DisplayName = "Chatter Api Scope",
                    Type = ScopeType.Resource,
                    Emphasize = false,
                    Enabled = true,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                StandardScopes.OfflineAccess
            };

            return scopes;
        }
    }
}