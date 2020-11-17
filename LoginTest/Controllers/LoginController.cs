using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LoginTest.Controllers
{
    public class LoginController : ControllerBase
    {
        private IMongoCollection<LoginData> gameCollection;
        public LoginController()
        {
            var client = new MongoClient("mongodb+srv://Admin:n3Bwr67PGYbL19ih@cluster0.rsuob.gcp.mongodb.net/<dbname>?retryWrites=true&w=majority");
            var database = client.GetDatabase("Test");
            gameCollection = database.GetCollection<LoginData>("Game");
        }/*
        public LoginController(MongoClient client)
        {
            var database = client.GetDatabase("Test");
            gameCollection = database.GetCollection<LoginData>("Game");
        }*/
        [HttpGet]
        [Route("getAllData")]
        public IEnumerable<LoginData> Get()
        {
            var filterAll = Builders<LoginData>.Filter.Empty;
            return gameCollection.Find(filterAll).ToList();
        }

        [HttpPost]
        [Route("register")]
        public IEnumerable<LoginData> Register([FromBody] LoginData loginData)
        {
            if (String.IsNullOrEmpty(loginData.telNo) && String.IsNullOrEmpty(loginData.firstName) && String.IsNullOrEmpty(loginData.lastname)
                && String.IsNullOrEmpty(loginData.bankAccountNumber) && String.IsNullOrEmpty(loginData.lineId) && String.IsNullOrEmpty(loginData.passWord))
            {
                return null;
            }
            else { 
                /// get collection data
                var sort = Builders<LoginData>.Sort.Descending("_id");
                var filterAll = Builders<LoginData>.Filter.Empty;
                var results = gameCollection.Find(filterAll);
                if(!results.ToList().Any())
                {
                  loginData.userName = "user000000";
                  gameCollection.InsertOne(loginData);
                }
                else
                {
                    /// count collection data +1 userName
                    var count = results.ToList().Count + 1;
                    var userNumber = count.ToString().PadLeft(6, '0');
                    loginData.userName = "user" + userNumber;
                    gameCollection.InsertOne(loginData);
                }
            
                return gameCollection.Find(s => s.firstName == loginData.firstName).ToList();
            }
        }

        [HttpPost]
        [Route("test")]
        public IEnumerable<LoginData> Test([FromBody]LoginData loginData)
        { 
            return gameCollection.Find(s => s.firstName == loginData.firstName).ToList();
        }

        [HttpPost]
        [Route("login")]
        public bool GetLoginData([FromBody] LoginData loginData)
        {
            try { 
                var results = gameCollection.Find(s => s.userName == loginData.userName && s.passWord == loginData.passWord).ToList();
                return results.ToList().Any();
            }
            catch (Exception) 
            {
                return false;
            }
        }
    }
}
