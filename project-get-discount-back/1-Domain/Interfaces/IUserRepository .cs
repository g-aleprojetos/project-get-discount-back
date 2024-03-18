using project_get_discount_back.Entities;
using System.Threading.Tasks;

namespace project_get_discount_back.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User?> GetByEmailPassword(string email, CancellationToken cancellationToken);
    }
}
