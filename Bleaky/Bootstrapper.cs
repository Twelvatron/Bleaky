using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bleaky.Infrastructure;
using Bleaky.Infrastructure.Authentication;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.Session;

namespace Bleaky
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            pipelines.OnError.AddItemToEndOfPipeline(OnError);

            CookieBasedSessions.Enable(pipelines);

            pipelines.BeforeRequest.AddItemToStartOfPipeline(Auth.GetLoadAuthenticationHook(container.Resolve<IUserMapper>()));
            pipelines.AfterRequest.AddItemToEndOfPipeline(Auth.GetRedirectToLoginHook());
            pipelines.AfterRequest.AddItemToEndOfPipeline(CorrectEncoding);
        }

        void CorrectEncoding(NancyContext nancyContext)
        {
            if (nancyContext.Response.ContentType.StartsWith("application/json"))
                nancyContext.Response.ContentType = "application/json;charset=utf-8";
        }

        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Content"));
        }

        Response OnError(NancyContext nancyContext, Exception exception)
        {
            return nancyContext.Response;
        }
    }
}