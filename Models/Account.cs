using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoApp_API.Models
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public bool isActive { get; set; } = false!;
    }
}
