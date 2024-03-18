using project_get_discount_back.Entities;
using static project_get_discount_back._1_Domain.Helpers.Email;

namespace project_get_discount_back._1_Domain.Interfaces
{
    public interface IEmail
    {
        Task<bool> SendEmailAsync(User user, EmailType emailType);
    }
}
