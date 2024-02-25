using project_get_discount_back.Entities;

namespace project_get_discount_back._1_Domain.Interfaces
{
    public interface IEmail
    {
        Task<bool> SendEmailAsync(User user, string subject, string messageBody);
    }
}
