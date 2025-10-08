using MotoFacilAPI.Domain.Entities;

namespace MotoFacilAPI.Domain.Repositories
{
    public interface IMotoRepository
    {
        Task<Moto?> GetByIdAsync(int id);
        Task<List<Moto>> ListAsync();
        Task AddAsync(Moto moto);
        Task UpdateAsync(Moto moto);
        Task DeleteAsync(int id);
    }
}