using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using Bleaky.Infrastructure.Authentication;

namespace Bleaky.Modules
{
    public class WorkoutModule : NancyModule
    {
        public WorkoutModule() : base("/workout")
        {
            this.RequiresAuthentication();

            Get["/"] = WorkoutHome;
        }

        dynamic WorkoutHome(dynamic parameters)
        {
            return View["/Index.cshtml", Context.GetCurrentUser()];
        }
    }
}