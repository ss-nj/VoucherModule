using Application.Common.Models;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    // Infrastructure/Repositories/GenericRepository.cs
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly VoucherModuleDbContext _context;
        private readonly IMapper _mapper;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(VoucherModuleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<T>();
        }

        public async Task<TResult> GetByIdAsync<TResult>(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            return _mapper.Map<TResult>(entity);
        }
        public Task<TResult> GetSingleAsync<TResult>(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<T> GetSingleAsync(int id)//for delete update
        {
            var result = await _dbSet
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            if (result is null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            return result;
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters, Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // If you have extra filters
            if (filter != null)
                query = query.Where(filter);

            var totalCount = await query.CountAsync();
            var currentPage = Math.Max(parameters.Page, 1);

            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                Data = items,
                TotalCount = totalCount,
                PageNumber = parameters.Page,
                PageSize = parameters.PageSize
            };
        }


        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {//TODO:exception midlware
            var entity = _mapper.Map<T>(source);
            //try
            //{
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
            //                          (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            //{
            //    // Unique constraint violation at SQL Server
            //    throw new BadRequestException("NationalId or PhoneNumber already exists.");
            //}
            //catch (Exception ex)
            //{
            //    throw new BadRequestException(ex.Message);
            //}
            

            return _mapper.Map<TResult>(entity);
        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
        {
            var entity = await GetSingleAsync(id);

            _mapper.Map(source, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        //TODO:add patch

        public async Task DeleteAsync(int id)
        {
            var entity = await GetSingleAsync(id);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity != null;
        }

    }

}
