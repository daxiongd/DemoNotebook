using DemoNotebook.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoNotebook.Shared.Contract;

namespace DemoNotebook.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<TEntity>> AddAsync(TEntity entity);

        Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(MyQueryParameter parameter);
    }
}
