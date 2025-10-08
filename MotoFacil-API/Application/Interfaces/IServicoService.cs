
using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Interfaces
{
    public interface IServicoService
    {
        Task<List<ServicoDto>> ListAsync();
        Task<ServicoDto?> GetByIdAsync(int id);
        Task<ServicoDto> CreateAsync(ServicoDto dto);
        Task<bool> UpdateAsync(int id, ServicoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}