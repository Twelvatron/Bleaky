using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Cookies;
using Nancy.Extensions;
using Nancy.Helpers;
using Bleaky.Domain;

namespace Bleaky.Infrastructure.Authentication
{
    public static class Auth
    {
        public static Action<NancyContext> GetRedirectToLoginHook()
        {
            return context =>
                {
                    if (context.Response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        context.Response = context.GetRedirect(string.Format("{0}?{1}={2}",
                            "~/login",
                            "returnUrl",
                            context.ToFullPath("~" + context.Request.Path + HttpUtility.UrlEncode(context.Request.Url.Query))));
                    }
                };
        }

        public static Func<NancyContext, Response> GetLoadAuthenticationHook(IUserMapper userMapper)
        {
            return context =>
            {
                var userGuid = GetAuthenticatedUserFromCookie(context);

                if (userGuid != Guid.Empty)
                {
                    context.CurrentUser = userMapper.GetUserIdentityFromIdentifier(userGuid);
                }
                return null;
            };
        }

        public static Response LoginAndRedirect(this NancyModule module, Guid userIdentifier, DateTime? cookieExpiry = null)
        {
            string redirectUrl = "~/";

            if (module.Context.Request.Query["returnUrl"].HasValue)
            {
                redirectUrl = module.Context.Request.Query["returnUrl"];
            }

            var response = module.Context.GetRedirect(redirectUrl);
            var authenticationCookie = BuildCookie(userIdentifier, cookieExpiry);
            response.AddCookie(authenticationCookie);

            return response;
        }

        public static Response LougoutAndRedirect(this NancyModule module, string redirectUrl = "~/")
        {
            var response = module.Context.GetRedirect(redirectUrl);
            var authenticationCookie = BuildLogoutCookie();
            response.AddCookie(authenticationCookie);
            return response;
        }

        static INancyCookie BuildCookie(Guid userIdentifier, DateTime? cookieExpiry)
        {
            var cookie = new NancyCookie("_bleakyAuth", userIdentifier.ToString(), true) { Expires = cookieExpiry };
            return cookie;
        }

        static INancyCookie BuildLogoutCookie()
        {
            return new NancyCookie("_bleaktAuth", string.Empty, true) { Expires = DateTime.Now.AddDays(-1) };
        }

        static Guid GetAuthenticatedUserFromCookie(NancyContext context)
        {
            if (!context.Request.Cookies.ContainsKey("_bleakyAuth"))
            {
                return Guid.Empty;
            }

            string cookieValue = context.Request.Cookies["_bleakyAuth"];
            Guid returnGuid;
            if (string.IsNullOrEmpty(cookieValue) || !Guid.TryParse(cookieValue, out returnGuid))
            {
                return Guid.Empty;
            }
            return returnGuid;
        }

        public static User GetCurrentUser(this NancyContext context)
        {
            if (context.CurrentUser == null)
            {
                return null;
            }
            return context.CurrentUser as User;
        }
    }
}