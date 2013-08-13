using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Bleaky.Models;

namespace Bleaky.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule() : base("/Login")
        {
            Get["/"] = Login;
            Post["/Register"] = Register;
        }

        dynamic Login(dynamic parameters)
        {
            return View["/Index.cshtml"];
        }

        dynamic Register(dynamic parameters)
        {
            var registerUser = this.Bind<RegisterUser>();
            return "Hello";
        }
    }
}