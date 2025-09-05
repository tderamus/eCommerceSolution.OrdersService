
using MongoDB.Bson.Serialization.Attributes;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid _id { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid OrderId { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserId { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public DateTime OrderDate { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
