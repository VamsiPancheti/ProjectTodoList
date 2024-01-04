using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
public class TodoItemsSS
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string TaskName { get; set; } = null!;
    public bool IsCompleted { get; set; }
    public string Author { get; set; } = null!;

    public DateTime Date {get; set;} = DateTime.Now;

    public string Reviews {get; set;} = "No reviews available at the moment";
}
}