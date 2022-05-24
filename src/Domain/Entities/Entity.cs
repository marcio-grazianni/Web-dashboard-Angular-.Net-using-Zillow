using System;

namespace CDACommercial.PoC.Domain.Entities
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

    }


    public enum EntityType
    {
        City,
        Listing
    }
}