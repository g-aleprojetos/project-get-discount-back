using project_get_discount_back.Common;
using project_get_discount_back.Helpers;

namespace project_get_discount_back._1_Domain.Entities
{
    public class Password : BaseEntity
    {
        public required string PasswordHash { get; set; }
        public Guid UserId { get; set; }


        public Password(string password, string createdBy, Guid userId) : base()
        {
            PasswordHash = Encrypting(password);
            CreatedBy = createdBy;
            CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            UserId = userId;
        }

        public string Encrypting(string valor)
        {
            var encryptedPassword = new Cryptography();
            return encryptedPassword.Encrypt(valor);
        }
    }
}
