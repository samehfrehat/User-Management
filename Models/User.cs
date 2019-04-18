using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace UsersManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string _id { get; set; }

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("address")]
        public Address address { get; set; }

        [BsonElement("phone")]
        public string phone { get; set; }

        [BsonElement("website")]
        public string website { get; set; }

        [BsonElement("company")]
        public Company company { get; set; }


    }

    public class Address
    {
        [BsonElement("street")]
        public string street { get; set; }

        [BsonElement("suite")]
        public string suite { get; set; }

        [BsonElement("city")]
        public string city { get; set; }

        [BsonElement("zipcode")]
        public string zipcode { get; set; }

        [BsonElement("geo")]
        public Geo geo { get; set; }
    }

    public class Geo
    {
        [BsonElement("lat")]
        public string lat { get; set; }

        [BsonElement("lng")]
        public string lng { get; set; }
    }

    public class Company
    {
        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("catchphrase")]
        public string catchphrase { get; set; }

        [BsonElement("bs")]
        public string bs { get; set; }
    }

}
