using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Services.InMemory;

namespace ChatterApi.IdSvr.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser
                {
                    Username = "Rob",
                    Password = "secret",
                    Subject = "1",
                    Claims = new []
                    {
                        new Claim("GivenName", "Rob"),
                        new Claim("FamilyName", "Washington")
                    }
                },

                new InMemoryUser
                {
                    Username = "David",
                    Password = "secret",
                    Subject = "2",
                    Claims = new []
                    {
                        new Claim("GivenName", "David"),
                        new Claim("FamilyName", "Washington")
                    }
                }

            };
        }
    }
}