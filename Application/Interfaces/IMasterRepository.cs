using Application.DTOs.MasterDtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMasterRepository :IGenericRepository<Master>
    {
        Task<MasterDto> GetDetailsById(int id);
         Task<List<object>> GetReportAsync(int id);
    }
}
