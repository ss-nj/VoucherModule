using Application.Common.Models;
using Application.DTOs.MasterDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<GetMasterDto>> GetAllAsync(QueryParameters parameters)
        {
            return await _repository.GetAllAsync<GetMasterDto>(parameters);
        }

        public async Task<GetMasterDto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync<GetMasterDto>(id);
        }

        public async Task<MasterDto> GetDetailsByIdAsync(int id)
        {
            return await _repository.GetDetailsById(id);
        }

        public async Task<object> GetReportAsync(int id)
        {
            return await _repository.GetReportAsync(id);
        }

        public async Task<MasterDto> CreateAsync(CreateMasterDto dto)
        {
            return await _repository.AddAsync<CreateMasterDto, MasterDto>(dto);
        }

        public async Task UpdateAsync(int id, UpdateMasterDto dto)
        {
            await _repository.UpdateAsync(id, dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


    }
}
