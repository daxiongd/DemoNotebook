using DemoNotebook.Shared.Parameters;
namespace DemoNotebook.Api.Services
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync(QueryParameter queryParameter);
        Task<ApiResponse> GetSingleAsync(int id);
        Task<ApiResponse> AddAsync(T Model);
        Task<ApiResponse> UpdateAsync(T model);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
