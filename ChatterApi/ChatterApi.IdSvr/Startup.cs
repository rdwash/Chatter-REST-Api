using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Security.Cryptography.X509Certificates;
using ChatterApi.Constants;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;

[assembly: OwinStartup(typeof(ChatterApi.IdSvr.Startup))]

namespace ChatterApi.IdSvr
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.Map("/identity", idsvrApp =>
            {
                idsvrApp.UseIdentityServer(new IdentityServerOptions
                {

                    SiteName = "Embedded IdentityServer",
                    IssuerUri = ChatterApiConstants.IdSrvIssuerUri,
                    Factory = new IdentityServerServiceFactory().Configure(),
                    SigningCertificate = LoadCertificate()
                });
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}