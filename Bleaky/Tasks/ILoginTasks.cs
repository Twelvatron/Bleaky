using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Bleaky.Domain;

namespace Bleaky.Tasks
{
    public interface ILoginTasks
    {
        bool RegisterUser(NewUser newUser);
        string showConfig();
    }

    public class LoginTasks : ILoginTasks
    {
        public bool RegisterUser(NewUser newUser)
        {
            throw new NotImplementedException();
        }

        public string showConfig()
        {
            return ConfigurationManager.AppSettings["test"];
        }
    }
}