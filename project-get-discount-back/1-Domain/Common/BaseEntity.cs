namespace project_get_discount_back.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
