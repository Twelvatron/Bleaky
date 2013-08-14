using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Bleaky.Domain;
using Bleaky.Data;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Bleaky.Tasks
{
    public interface ILoginTasks
    {
        Tuple<bool, string> RegisterUser(NewUser newUser);
    }

    public class LoginTasks : ILoginTasks
    {
        readonly MongoDatabase _database;

        public LoginTasks(IBleakyDatabase database)
        {
            _database = database.GetDatabase();
        }

        public Tuple<bool, string> RegisterUser(NewUser newUser)
        {
            var collection = _database.GetCollection<User>("Users");
            var query = Query.EQ("Email", newUser.Email);
            var result = collection.Find(query);

            if (result.Size() > 0)
            {
                return new Tuple<bool, string>(false, "A user with that email address already exists, please try using a different email address");
            }

            collection.Insert(new User { Name = Guid.NewGuid().ToString(), Email = newUser.Email, Password = newUser.Password });
            return new Tuple<bool,string>(true, "User successfully created");
        }
    }
}