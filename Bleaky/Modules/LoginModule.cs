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
        readonly ILoginTasks _loginTasks;

        public LoginModule(ILoginTasks loginTasks) : base("/Login")
        {
            _loginTasks = loginTasks;

            Get["/"] = Login;
            Post["/Register"] = Register;
        }

        dynamic Login(dynamic parameters)
        {
            return View["/Index.cshtml"];
        }

        dynamic Register(dynamic parameters)
        {
            var registerUser = this.Bind<NewUser>();

            var result = _loginTasks.RegisterUser(registerUser);
            return Response.AsJson(result);            
        }
    }
}