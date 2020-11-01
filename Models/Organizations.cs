using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Models
{
    // public class Pricing 
    // {
    //     Dictionary<int, int> pricing = new Dictionary<int, int>();
    // }

    // public class Timestamp
    // {
    //     [BsonElement("_seconds")]
    //     public int _seconds { get; set; }

    //     [BsonElement("_nanoseconds")]
    //     public int _nanoseconds { get; set; }
    // }

    // public class Modified
    // {
    //     [BsonElement("author")]
    //     public string author { get; set; }
    //     [BsonElement("createdByUid")]
    //     public ObjectId createdByUid { get; set; }
    //     public Timestamp timestamp { get; set; }    
        
    // }
    public class Organizations
    {
        // public ObjectId ObjectId { get; set; }

        // [BsonElement("customerId")]
        // public ObjectId customerId { get; set; }

        // [BsonElement("author")]
        // public string author { get; set; }

        // [BsonElement("createdByUid")]
        // public ObjectId createdByUid { get; set; }
        
        // [BsonElement("customerName")]
        // public string customerName { get; set; }

        // [BsonElement("enabled")]
        // public bool enabled { get; set; }

        // [BsonElement("ico")]
        // public string ico { get; set; }

        // public Pricing pricing { get; set; }
        // public Modified modified { get; set; }
        // public Timestamp timestamp { get; set; }
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DressCode { get; set; }
        public byte[] Logo { get; set; }

    }
}