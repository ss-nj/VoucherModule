using Application.Common.Models;
using Application.DTOs.SubsidiaryDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class SubsidiaryService : ISubsidiaryService
    {
        private readonly ISubsidiaryRepository _repository;

        public SubsidiaryService(ISubsidiaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<GetSubsidiaryDto>> GetAllAsync(QueryParameters parameters)
        {
            return await _repository.GetAllAsync<GetSubsidiaryDto>(parameters);
        }

        public async Task<GetSubsidiaryDto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync<GetSubsidiaryDto>(id);
        }

        public async Task<SubsidiaryDto> GetDetailsByIdAsync(int id)
        {
            return await _repository.GetDetailsById(id);
        }

        public async Task<SubsidiaryDto> CreateAsync(CreateSubsidiaryDto dto)
        {
            return await _repository.AddAsync<CreateSubsidiaryDto, SubsidiaryDto>(dto);
        }

        public async Task UpdateAsync(int id, UpdateSubsidiaryDto dto)
        {
            await _repository.UpdateAsync(id, dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
