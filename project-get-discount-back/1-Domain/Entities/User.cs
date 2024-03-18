using project_get_discount_back._1_Domain.Entities;
using project_get_discount_back.Common;
using System.ComponentModel.DataAnnotations;

namespace project_get_discount_back.Entities
{
    public class User(string name, string email, User.AccessType role) : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; private set; } = name;

        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 3)]
        public string Email { get; private set; } = email;

        [Required]
        public AccessType Role { get; private set; } = role;
        public Password? Password { get; private set; }
        public string? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public User(string name, string email, AccessType role, string userCreate) : this(name, email, role)
        {
            CreatedBy = userCreate;
            CreatedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public void ActivateUser(string userCreate)
        {
            Deleted = false;
            CreatedBy = userCreate;
        }

        public void UpdateUser(string name, string email, AccessType role, string updatedBy)
        {
            Name = name;
            Email = email;
            Role = role;
            UpdatedBy = updatedBy;
            UpdatedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public void SetPassword(Password password)
        {
            Password = password;
        }

        public enum AccessType
        {
            ADMIN,
            USER
        }
    }
}
