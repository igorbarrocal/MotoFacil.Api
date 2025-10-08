using Microsoft.EntityFrameworkCore;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Persistence;

namespace MotoFacilAPI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UsuarioRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<Usuario?> GetByIdAsync(int id) => await _ctx.Usuarios.Include(u => u.Motos).FirstOrDefaultAsync(u => u.Id == id);
        public async Task<List<Usuario>> ListAsync() => await _ctx.Usuarios.AsNoTracking().ToListAsync();
        public async Task AddAsync(Usuario usuario) { _ctx.Usuarios.Add(usuario); await _ctx.SaveChangesAsync(); }
        public async Task UpdateAsync(Usuario usuario) { _ctx.Usuarios.Update(usuario); await _ctx.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Usuarios.FindAsync(id);
            if (entity != null) { _ctx.Usuarios.Remove(entity); await _ctx.SaveChangesAsync(); }
        }
    }
}