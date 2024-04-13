namespace Gallery.Models.BaseEntityModel
{
    public abstract class BaseEntity { }
    public abstract class BaseEntity<KeyTypeId> : BaseEntity where KeyTypeId : struct
    {
        public KeyTypeId Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid? CreateUserId { get; set; }
        public Guid? LastUpdateUserId { get; set; }
        //public virtual User? CreateUser { get; set; }
        //public virtual User? LastUpdateUser { get; set; }

        public virtual BaseEntity<KeyTypeId> SetTraceableEntity(DateTime createDateTime, DateTime updateDateTime)
        {
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            return this;
        }

        public virtual BaseEntity<KeyTypeId> SetTraceableEntity(Guid createUserId, Guid lastUpdateUserId, DateTime createDateTime, DateTime updateDateTime)
        {
            CreateUserId = createUserId;
            LastUpdateUserId = lastUpdateUserId;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            return this;
        }
    }
}
