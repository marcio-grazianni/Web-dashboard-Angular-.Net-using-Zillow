namespace CDACommercial.PoC.Domain.Entities
{
    public class Like : Entity
    {
        public EntityType Type { get; set; }
        public long UserId { get; set; }
        public long EntityId { get; set; }

        public Like(long userId, long entityId, EntityType type = EntityType.Listing)
        {
            UserId = userId;
            EntityId = entityId;
            Type = type;
        }
    }
}