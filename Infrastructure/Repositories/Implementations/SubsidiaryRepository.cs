using Application.DTOs.SubsidiaryDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class SubsidiaryRepository : GenericRepository<Subsidiary>, ISubsidiaryRepository
    {
        private readonly VoucherModuleDbContext _context;
        private readonly IMapper _mapper;
        public SubsidiaryRepository(VoucherModuleDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<SubsidiaryDto> GetDetailsById(int id)
        {
            var Subsidiary = await _context.Subsidiaries
                .Include(q => q.Owner)
                .Include(q => q.ChildSubsidiaries)
                .Include(q => q.ParentSubsidiary)
                .Include(q => q.Master)
                .ProjectTo<SubsidiaryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (Subsidiary is null)
            {
                throw new NotFoundException(typeof(Subsidiary).Name, id);
            }

            return Subsidiary;
        }


        public async Task<object> GetReportAsync(int id)
        {
            var parent = await _context.Subsidiaries
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (parent == null)
            {
                throw new NotFoundException(typeof(Subsidiary).Name, id);
            }

            var children = await _context.Subsidiaries
                .AsNoTracking()
                .Where(s => !s.IsDeleted && s.ParentPath.Contains(id.ToString()) && s.Id != id)
                .ToListAsync();

            var totalDebit = parent.DebitAmount + children.Sum(s => s.DebitAmount);
            var totalCredit = parent.CreditAmount + children.Sum(s => s.CreditAmount);

            var report = new
            {
                SubsidiaryId = parent.Id,
                SubsidiaryTitle = parent.Title,
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                Subsidiaries = children.Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.DebitAmount,
                    s.CreditAmount
                }).ToList()
            };

            return report;
        }

    }
}
