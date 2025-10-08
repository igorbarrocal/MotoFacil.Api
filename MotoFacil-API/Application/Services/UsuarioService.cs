using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Interfaces;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Domain.ValueObjects;

namespace MotoFacilAPI.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo) => _repo = repo;

        public async Task<List<UsuarioDto>> ListAsync()
        {
            var usuarios = await _repo.ListAsync();
            return usuarios.Select(usuario => new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email.Value
            }).ToList();
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _repo.GetByIdAsync(id);
            return usuario is null ? null : new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email.Value
            };
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioDto dto)
        {
            var entity = new Usuario(dto.Nome, new Email(dto.Email));
            await _repo.AddAsync(entity);
            return new UsuarioDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Email = entity.Email.Value
            };
        }

        public async Task<bool> UpdateAsync(int id, UsuarioDto dto)
        {
            var usuario = await _repo.GetByIdAsync(id);
            if (usuario is null) return false;
            usuario.AlterarNome(dto.Nome);
            usuario.AlterarEmail(new Email(dto.Email));
            await _repo.UpdateAsync(usuario);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}