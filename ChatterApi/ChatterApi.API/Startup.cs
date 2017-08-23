using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ChatterApi.API.Helpers;
using ChatterApi.Constants;
using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityServer.AccessTokenValidation;

[assembly: OwinStartup(typeof(ChatterApi.API.Startup))]

namespace ChatterApi.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions {
                    Authority = ChatterApiConstants.IdSrv,
                    RequiredScopes = new [] { "chatterapi" }
                });


            app.UseWebApi(config);
        }
    }
}