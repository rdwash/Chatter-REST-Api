using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace ChatterApi.API.Helpers
{
    public class AuthorizationManager : ResourceAuthorizationManager
    {
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            switch (context.Resource.First().Value)
            {
                case "Message":
                    return AuthorizeMessage(context);
                default:
                    return Nok();
            }
        }

        private Task<bool> AuthorizeMessage(ResourceAuthorizationContext context)
        {
            switch (context.Resource.First().Value)
            {
                case "Read":
                    //to be able to read messages from the API, the user must be in the WebReadUser role or MobileReadUser role
                    return
                        Eval(context.Principal.HasClaim("role", "MobileReadUser")
                        || (context.Principal.HasClaim("role", "WebReadUser")));
                case "Write":
                    return Eval(context.Principal.HasClaim("role", "MobileWriteUser")
                        || (context.Principal.HasClaim("role", "WebWriteUser")));
                default:
                    return Nok();
            }
        }
    }
}