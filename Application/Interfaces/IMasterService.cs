using Application.Common.Models;
using global::Application.Common.Models;
using global::Application.DTOs.MasterDtos;

namespace Application.Interfaces
{

    public interface IMasterService
    {
        Task<PagedResult<GetMasterDto>> GetAllAsync(QueryParameters parameters);
        Task<GetMasterDto> GetByIdAsync(int id);
        Task<MasterDto> CreateAsync(CreateMasterDto dto);
        Task UpdateAsync(int id, UpdateMasterDto dto);
        Task DeleteAsync(int id);
    }

}
