using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Bleaky.Domain;
using Bleaky.Tasks;

namespace Bleaky.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule() : base("/Login")
        {
            Get["/"] = Login;
            Post["/Register"] = Register;
            Get["/Test"] = Test;
        }

        dynamic Login(dynamic parameters)
        {
            return View["/Index.cshtml"];
        }

        dynamic Register(dynamic parameters)
        {
            var registerUser = this.Bind<NewUser>();
            return "Hello";
        }

        dynamic Test(dynamic parameters)
        {
            var tasks = new LoginTasks();
            var result = tasks.showConfig();
            return result;
        }
    }
}