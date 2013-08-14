using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MongoDB.Driver;

namespace Bleaky.Data
{
    public interface IBleakyDatabase
    {
        MongoDatabase GetDatabase();
    }

    public class BleakyDatabase : IBleakyDatabase
    {
        readonly MongoDatabase _database;

        public BleakyDatabase()
        {
            var connectionString = ConfigurationManager.AppSettings["MONGOLAB_URI"];
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            var server = client.GetServer();
            _database = server.GetDatabase(url.DatabaseName);
        }

        public MongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}