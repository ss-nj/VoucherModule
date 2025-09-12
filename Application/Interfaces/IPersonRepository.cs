using Application.DTOs.PersonDtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPersonRepository :IGenericRepository<Person>
    {
        Task<PersonDto> GetDetailsById(int id);
    }
}
