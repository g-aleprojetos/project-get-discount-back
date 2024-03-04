using Microsoft.EntityFrameworkCore;
using project_get_discount_back.Context;
using project_get_discount_back.Entities;
using project_get_discount_back.Interfaces;

namespace project_get_discount_back.Repositories
{
    public class UserRepository(DataContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await Context.Users
                    .Include(u => u.Password)
                    .FirstOrDefaultAsync(x => x.Email == email && !x.Deleted && x.Password != null && !x.Password.Deleted, cancellationToken);

        }
    }
}
