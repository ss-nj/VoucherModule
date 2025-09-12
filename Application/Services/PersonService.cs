using Application.Common.Models;
using Application.DTOs.PersonDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<GetPersonDto>> GetAllAsync(QueryParameters parameters)
        {
            return await _repository.GetAllAsync<GetPersonDto>(parameters);
        }

        public async Task<GetPersonDto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync<GetPersonDto>(id);
        }

        public async Task<PersonDto> GetDetailsByIdAsync(int id)
        {
            return await _repository.GetDetailsById(id);
        }

        public async Task<PersonDto> CreateAsync(CreatePersonDto dto)
        {
            return await _repository.AddAsync<CreatePersonDto, PersonDto>(dto);
        }

        public async Task UpdateAsync(int id, UpdatePersonDto dto)
        {
            await _repository.UpdateAsync(id, dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
