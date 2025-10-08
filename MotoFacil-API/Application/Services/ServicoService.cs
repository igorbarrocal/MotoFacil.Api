using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Interfaces;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;

namespace MotoFacilAPI.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _repo;
        public ServicoService(IServicoRepository repo) => _repo = repo;

        public async Task<List<ServicoDto>> ListAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(s => new ServicoDto
            {
                Id = s.Id,
                Descricao = s.Descricao,
                Data = s.Data,
                UsuarioId = s.UsuarioId,
                MotoId = s.MotoId
            }).ToList();
        }

        public async Task<ServicoDto?> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s is null) return null;
            return new ServicoDto
            {
                Id = s.Id,
                Descricao = s.Descricao,
                Data = s.Data,
                UsuarioId = s.UsuarioId,
                MotoId = s.MotoId
            };
        }

        public async Task<ServicoDto> CreateAsync(ServicoDto dto)
        {
            var entity = new Servico(dto.Descricao, dto.Data, dto.UsuarioId, dto.MotoId);
            await _repo.AddAsync(entity);
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ServicoDto dto)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s is null) return false;
            s.Reagendar(dto.Data);
            await _repo.UpdateAsync(s);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}