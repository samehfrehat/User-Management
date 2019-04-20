using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagement.Models;

namespace UsersManagement.Services
{
    public class UserService
    {
        private  readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("UsersManagementDb"));
            var database = client.GetDatabase("UsersManagementDb");
            _users = database.GetCollection<User>("Users");           
        }
        
        public async Task insertManyUsersAsync(IEnumerable<User> users)
        {
            try
            {
               await _users.InsertManyAsync(users);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool checkEmpty()
        {
            if(_users.Find(user => true).FirstOrDefault() == null)
            return true;
            return false;
        }

        public List<User> search(User userIn)
        {
            List<User> foundUsers = new List<User>();
            var query = (from e in _users.AsQueryable<User>()
                        where 
                           e.name == userIn.name || e.username == userIn.username
                        || e.address.zipcode == userIn.address.zipcode || e.company.name == userIn.company.name
                        select e);
            foreach(User u in query)
            {                
                foundUsers.Add(u);
            }
            return foundUsers;
        }

        public List<User> Get()
        {                   
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
    
}
