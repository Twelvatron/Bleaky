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
using Bleaky.Infrastructure.Authentication;
using Nancy.Security;

namespace Bleaky.Tasks
{
    public interface ILoginTasks
    {
        Tuple<bool, string> LoginUser(NewUser newUser);
        Tuple<bool, string> RegisterUser(NewUser newUser);
    }

    public class LoginTasks : ILoginTasks, IUserMapper
    {
        readonly MongoDatabase _database;
        readonly IPasswordTasks _passwordTasks;

        public LoginTasks(IBleakyDatabase database, IPasswordTasks passwordTasks)
        {
            _database = database.GetDatabase();
            _passwordTasks = passwordTasks;
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

            var hashedPassword = _passwordTasks.CreateHash(newUser.Password);
            var userId = Guid.NewGuid();

            collection.Insert(new User 
                {
                  Name = userId.ToString(),
                  Email = newUser.Email, 
                  Password = hashedPassword, 
                  UserId = userId, 
                  UserType = UserType.User, 
                  UserName = newUser.Email
                });

            return new Tuple<bool, string>(true, userId.ToString());
        }

        public Tuple<bool, string> LoginUser(NewUser newUser)
        {
            var collection = _database.GetCollection<User>("Users");
            var query = Query.EQ("Email", newUser.Email);
            var user = collection.FindOne(query);

            if (user == null)
            {
                return new Tuple<bool, string>(false, "Invalid username or password");
            }

            var passwordResult = _passwordTasks.ValidatePassword(newUser.Password, user.Password);

            if (!passwordResult)
            {
                return new Tuple<bool, string>(false, "Invalid username or password");
            }
            return new Tuple<bool, string>(true, user.UserId.ToString());
        }

        public IUserIdentity GetUserIdentityFromIdentifier(Guid userId)
        {
            var collection = _database.GetCollection<User>("Users");
            var query = Query.EQ("UserId", userId);
            var user = collection.FindOne(query);

            return user == null ? null : user;
        }
    }
}