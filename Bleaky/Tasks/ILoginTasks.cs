using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Bleaky.Domain;
using Bleaky.Data;
using MongoDB.Driver;

namespace Bleaky.Tasks
{
    public interface ILoginTasks
    {
        bool RegisterUser(NewUser newUser);
    }

    public class LoginTasks : ILoginTasks
    {
        readonly MongoDatabase _database;

        public LoginTasks(IBleakyDatabase database)
        {
            _database = database.GetDatabase();
        }

        public bool RegisterUser(NewUser newUser)
        {
            var user = newUser;
            var collection = _database.GetCollection<User>("Users");

            collection.Insert(new User { Name = newUser.Email, Email = newUser.Email, Password = newUser.Password });
            collection.Drop();
            var users = collection.FindAll();

            return true;
        }
    }
}