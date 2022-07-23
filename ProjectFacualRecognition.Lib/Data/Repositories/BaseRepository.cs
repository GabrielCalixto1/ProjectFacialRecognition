using Microsoft.EntityFrameworkCore;
using ProjectFacualRecognition.Lib.Data.Repositories.Interfaces;
using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacualRecognition.Lib.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : ModelBase
    {
        protected readonly ProjectFacialRecognitionContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(DbSet<T> dbset, ProjectFacialRecognitionContext context)
        {
            _context = context;
            _dbSet = dbset;
        }

        public async Task CreateUser(T user)
        {
           await _dbSet.AddAsync(user);
           await _context.SaveChangesAsync();
        }

        public async Task<T> GetUserById(int id)
        {
           return await _dbSet.AsNoTracking().FirstAsync(x => x.Id == id);
        }
      
          public async Task<List<T>> GetAllUsers()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task DeleteUserById(int id)
        {
            var item = await _dbSet.AsNoTracking().FirstAsync(x => x.Id == id);
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}