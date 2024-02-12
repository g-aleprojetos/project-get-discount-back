using Microsoft.EntityFrameworkCore;
using project_get_discount_back.Context;
using project_get_discount_back.Entities;
using project_get_discount_back.Interfaces;

namespace project_get_discount_back.Repositories
{
    public class UserRepository(ApiDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
    }
}
