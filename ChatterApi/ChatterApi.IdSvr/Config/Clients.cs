﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatterApi.Constants;
using IdentityServer3.Core.Models;

namespace ChatterApi.IdSvr.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client {
                    Enabled = true,
                    ClientName = "MVC Client (Hybrid Flow)",
                    ClientId = "mvc",
                    Flow = Flows.Hybrid,
                    RequireConsent = true,
                    RedirectUris = new List<string> { ChatterApiConstants.ChatterApiClient }
                    

                }
            };
        }
    }
}