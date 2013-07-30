using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Bleaky.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule() : base("/Login")
        {
            Get["/"] = Login;
        }

        dynamic Login(dynamic parameters)
        {
            return View["/Index.cshtml"];
        }
    }
}