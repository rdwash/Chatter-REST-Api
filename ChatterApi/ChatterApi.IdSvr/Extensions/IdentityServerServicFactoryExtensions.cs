using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatterApi.IdSvr.Config;
using IdentityServer3.Core.Configuration;


namespace IdentityServer3.Core.Configuration
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory)
        {
            factory.UseInMemoryClients(Clients.Get());
            factory.UseInMemoryUsers(Users.Get());
            factory.UseInMemoryScopes(Scopes.Get());

            return factory;
        }
    }
}