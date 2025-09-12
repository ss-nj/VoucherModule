using Application.DTOs.MasterDtos;
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
    public class MasterRepository :GenericRepository<Master>, IMasterRepository
    {
        private readonly VoucherModuleDbContext _context;
        private readonly IMapper _mapper;
        public MasterRepository(VoucherModuleDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<MasterDto> GetDetailsById(int id)
        {
            var Master = await _context.Masters
                .Include(q => q.Owner)
                .Include(q => q.Subsidiaries)
                .ProjectTo<MasterDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (Master is null)
            {
                throw new NotFoundException(typeof(Master).Name, id);
            }

            return Master;
        }
    }
}
