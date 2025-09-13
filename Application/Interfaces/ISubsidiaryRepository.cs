using Application.DTOs.SubsidiaryDtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISubsidiaryRepository :IGenericRepository<Subsidiary>
    {
        Task<SubsidiaryDto> GetDetailsById(int id);
        Task<object> GetReportAsync(int id);
    }
}
