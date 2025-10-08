using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ListAsync();
        Task<UsuarioDto?> GetByIdAsync(int id);
        Task<UsuarioDto> CreateAsync(UsuarioDto dto);
        Task<bool> UpdateAsync(int id, UsuarioDto dto);
        Task<bool> DeleteAsync(int id);
    }
}