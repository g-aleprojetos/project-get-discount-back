using project_get_discount_back.Common;
using project_get_discount_back.Helpers;

namespace project_get_discount_back._1_Domain.Entities
{
    public class Password(string passwordHash) : BaseEntity
    {
        public string PasswordHash { get; set; } = passwordHash;
        public Guid UserId { get; set; }

        public Password(string password, Guid userId) : this(Encrypting(password))
        {
            UserId = userId;
            CreatedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public Password(string password, Guid userId, string createdBy) : this(Encrypting(password), userId)
        {
            CreatedBy = createdBy;
            CreatedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        private static string Encrypting(string valor)
        {
            var encryptedPassword = new Cryptography();
            return encryptedPassword.Encrypt(valor);
        }
    }

}
