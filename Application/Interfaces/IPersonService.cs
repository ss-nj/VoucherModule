using Application.Common.Models;
using global::Application.Common.Models;
using global::Application.DTOs.PersonDtos;

namespace Application.Interfaces
{

    public interface IPersonService
    {
        Task<PagedResult<GetPersonDto>> GetAllAsync(QueryParameters parameters);
        Task<GetPersonDto> GetByIdAsync(int id);
        Task<PersonDto> CreateAsync(CreatePersonDto dto);
        Task UpdateAsync(int id, UpdatePersonDto dto);
        Task DeleteAsync(int id);
    }

}
