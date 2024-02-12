namespace project_get_discount_back.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
