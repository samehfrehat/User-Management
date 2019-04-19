﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagement.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace UsersManagement.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("UsersManagementDb"));
            var database = client.GetDatabase("UsersManagementDb");
            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get()
        {                   
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _users.Find<User>(user => user.Id == id).FirstOrDefault();
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
