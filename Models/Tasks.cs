using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TodoApp_API.Models
{
    public class Tasks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _Id { get; set; }
        public string taskDesc { get; set; } = null!;
        public string accountEmail { get; set; } = null!;
        public string state { get; set; } = null!;
    }
}
