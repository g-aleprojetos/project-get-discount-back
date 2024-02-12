using project_get_discount_back.Entities;

namespace project_get_discount_back.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    }
}
