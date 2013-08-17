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

        public LoginModule(ILoginTasks loginTasks) : base("/login")
        {
            _loginTasks = loginTasks;

            Get["/"] = LoginHome;
            Post["/"] = Login;
            Post["/register"] = Register;
        }

        dynamic LoginHome(dynamic parameters)
        {
            return View["/Index.cshtml"];
        }

        dynamic Login(dynamic parameters)
        {
            var newUser = this.Bind<NewUser>();
            var result = _loginTasks.LoginUser(newUser);
            return Response.AsJson( new { Success = result.Item1, Message = result.Item2 });
        }

        dynamic Register(dynamic parameters)
        {
            var newUser = this.Bind<NewUser>();
            var result = _loginTasks.RegisterUser(newUser);
            return Response.AsJson( new {Success = result.Item1, Message = result.Item2});            
        }
    }
}