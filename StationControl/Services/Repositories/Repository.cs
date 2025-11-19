using Microsoft.EntityFrameworkCore;
using StationControl.Data;
using StationControl.Services.Interfaces;

namespace StationControl.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StationControlDbContext _context;

        public Repository(StationControlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
