using Application.Common.Models;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    // Infrastructure/Repositories/IGenericRepository.cs
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<TResult> GetByIdAsync<TResult>(int id);
        Task<T> GetSingleAsync(int id);
        //Task<List<TResult>> GetAllAsync<TResult>();
        Task<TResult> AddAsync<TSource, TResult>(TSource source);
        Task DeleteAsync(int id);
        Task UpdateAsync<TSource>(int id, TSource source);
        Task<bool> Exists(int id);
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters, Expression<Func<T, bool>>? filter = null);
    }

}
