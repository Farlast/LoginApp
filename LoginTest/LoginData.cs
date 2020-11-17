using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LoginTest
{
    [BsonIgnoreExtraElements]
    public class LoginData
    {
        [BsonId]
        public ObjectId id { get; set; }

        [BsonElement("telNo")]
        public string telNo { get; set; }

        [BsonElement("firstName")]
        public string firstName { get; set; }
        
        [BsonElement("lastname")]
        public string lastname { get; set; }

        [BsonElement("bankAccountNumber")]
        public string bankAccountNumber { get; set; }

        [BsonElement("lineId")]
        public string lineId { get; set; }

        [BsonElement("userName")]
        public string userName { get; set; }

        [BsonElement("passWord")]
        public string passWord { get; set; }
    }
}
