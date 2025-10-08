using Microsoft.EntityFrameworkCore;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Persistence;

namespace MotoFacilAPI.Infrastructure.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ServicoRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<Servico?> GetByIdAsync(int id) => await _ctx.Servicos.FirstOrDefaultAsync(s => s.Id == id);
        public async Task<List<Servico>> ListAsync() => await _ctx.Servicos.AsNoTracking().ToListAsync();
        public async Task AddAsync(Servico servico) { _ctx.Servicos.Add(servico); await _ctx.SaveChangesAsync(); }
        public async Task UpdateAsync(Servico servico) { _ctx.Servicos.Update(servico); await _ctx.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Servicos.FindAsync(id);
            if (entity != null) { _ctx.Servicos.Remove(entity); await _ctx.SaveChangesAsync(); }
        }
    }
}