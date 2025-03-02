using MongoDB.Bson.Serialization.Attributes;

namespace InnoAgePotelApi.Models
{
    public class Poll
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Description { get; set; }

        public int TotalVotes { get; set; }
        public List<PollsYes> PollYes { get; set; }
        public List<PollsYes> PollNo { get; set; }


        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class PollsYes
    {
        public string? UserId { get; set; }
    }
}
