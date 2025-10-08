using Microsoft.EntityFrameworkCore;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Persistence;

namespace MotoFacilAPI.Infrastructure.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationDbContext _ctx;
        public MotoRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<Moto?> GetByIdAsync(int id) => await _ctx.Motos.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<List<Moto>> ListAsync() => await _ctx.Motos.AsNoTracking().ToListAsync();
        public async Task AddAsync(Moto moto) { _ctx.Motos.Add(moto); await _ctx.SaveChangesAsync(); }
        public async Task UpdateAsync(Moto moto) { _ctx.Motos.Update(moto); await _ctx.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Motos.FindAsync(id);
            if (entity != null) { _ctx.Motos.Remove(entity); await _ctx.SaveChangesAsync(); }
        }
    }
}