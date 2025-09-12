using Application.DTOs.PersonDtos;
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
    public class PersonRepository :GenericRepository<Person>, IPersonRepository
    {
        private readonly VoucherModuleDbContext _context;
        private readonly IMapper _mapper;
        public PersonRepository(VoucherModuleDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<PersonDto> GetDetailsById(int id)
        {
            var Person = await _context.Persons
                .Include(q => q.MastersCreated)
                .Include(q => q.SubsidiariesCreated)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (Person is null)
            {
                throw new NotFoundException(typeof(Person).Name, id);
            }

            return Person;
        }
    }
}
