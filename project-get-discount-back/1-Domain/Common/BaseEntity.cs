namespace project_get_discount_back.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public bool Deleted { get; set; } = false;

    }
}
