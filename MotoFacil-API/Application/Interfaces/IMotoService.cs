using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Interfaces
{
    public interface IMotoService
    {
        Task<List<MotoDto>> ListAsync();
        Task<MotoDto?> GetByIdAsync(int id);
        Task<MotoDto> CreateAsync(MotoDto dto);
        Task<bool> UpdateAsync(int id, MotoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}