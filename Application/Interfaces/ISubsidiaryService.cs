using Application.Common.Models;
using global::Application.Common.Models;
using global::Application.DTOs.SubsidiaryDtos;

namespace Application.Interfaces
{

    public interface ISubsidiaryService
    {
        Task<PagedResult<GetSubsidiaryDto>> GetAllAsync(QueryParameters parameters);
        Task<GetSubsidiaryDto> GetByIdAsync(int id);
        Task<SubsidiaryDto> CreateAsync(CreateSubsidiaryDto dto);
        Task UpdateAsync(int id, UpdateSubsidiaryDto dto);
        Task DeleteAsync(int id);
    }

}
