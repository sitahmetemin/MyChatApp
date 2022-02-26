using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyChatApp.Domain.Abstract.Base.Entities;

namespace MyChatApp.Domain.Concrete.Base.Entities
{
    public class Audit : IAudit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
    }
}
