using project_get_discount_back.Context;
using project_get_discount_back.Interfaces;

namespace project_get_discount_back.Repositories
{
    public class UnitOfWork(DataContext context) : IUnitOfWork
    {
        private readonly DataContext _context = context;

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
