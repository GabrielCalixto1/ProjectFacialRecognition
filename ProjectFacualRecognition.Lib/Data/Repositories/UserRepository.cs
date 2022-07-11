using Microsoft.EntityFrameworkCore;
using ProjectFacualRecognition.Lib.Data.Repositories.Interfaces;
using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacualRecognition.Lib.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
       
        public UserRepository(DbSet<User> dbset, ProjectFacialRecognitionContext context) : base(dbset, context)
        {
            
        }
        public async Task UpdateEmailUserById(int id, string email)
        {
            var user = await _dbSet.AsNoTracking().FirstAsync(x => x.Id == id);
            user.SetEmail(email);
            await _context.SaveChangesAsync();
        }
    }
}