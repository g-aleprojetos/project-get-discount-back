using project_get_discount_back._1_Domain.Entities;
using project_get_discount_back.Common;
using System.ComponentModel.DataAnnotations;

namespace project_get_discount_back.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public AccessType Role { get; set; }
        public Password? Password { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public User(string name, string email, AccessType role, string userCreate) : base()
        {
            Name = name;
            Email = email;
            Role = role;
            CreatedBy = userCreate;
            CreatedAt = new DateTimeOffset(DateTime.UtcNow);
        }
        public void ActivateUser(string userCreate)
        {
            Deleted = false;
            CreatedBy = userCreate;
        }

        public enum AccessType
        {
            ADMIN,
            USER,
            NULL
        }
    }
}
