using EmbeddedMvc.IdentityServer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Web.Helpers;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityModel.Client;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(EmbeddedMvc.Startup))]

namespace EmbeddedMvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            

            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.Map("/identity", idsrvApp =>
                {
                    idsrvApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Schowek Firmowy",
                        SigningCertificate = LoadCertificate(),

                        Factory = new IdentityServerServiceFactory()
                                    .UseInMemoryUsers(Uzytkownik.Get())
                                    .UseInMemoryClients(Clients.Get())
                                    .UseInMemoryScopes(Grupy.Get()),

                    });
                });

            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies"
                });


            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    Authority = "https://localhost:44319/identity",

                    ClientId = "mvc",
                    Scope = "openid profile roles sampleApi",
                    ResponseType = "id_token token",
                    RedirectUri = "https://localhost:44319/",

                    SignInAsAuthenticationType = "Cookies",
                    UseTokenLifetime = false,

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        SecurityTokenValidated = async n =>
                            {
                                var nid = new ClaimsIdentity(
                                    n.AuthenticationTicket.Identity.AuthenticationType,
                                    Constants.ClaimTypes.GivenName,
                                    Constants.ClaimTypes.Role);

                               
                                var userInfoClient = new UserInfoClient(
                                    new Uri(n.Options.Authority + "/connect/userinfo"),
                                    n.ProtocolMessage.AccessToken);

                                var userInfo = await userInfoClient.GetAsync();
                                userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                               
                                nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                                nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                              
                                nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                              
                                nid.AddClaim(new Claim("app_specific", "some data"));

                                n.AuthenticationTicket = new AuthenticationTicket(
                                    nid,
                                    n.AuthenticationTicket.Properties);
                            },

                            RedirectToIdentityProvider = n =>
                                {
                                    if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                                    {
                                        var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                                        if (idTokenHint != null)
                                        {
                                            n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                                        }
                                    }

                                    return Task.FromResult(0);
                                }
                    }
                });
        }

       

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}