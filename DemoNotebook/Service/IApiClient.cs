using System.Threading.Tasks;
using DemoNotebook.Shared.Contract;

public interface IApiClient
{
    Task<ApiResponse<T>> GetAsync<T>(string endpoint, object parameters = null);
    Task<ApiResponse<T>> PostAsync<T>(string endpoint, object body);
    Task<ApiResponse<T>> PutAsync<T>(string endpoint, object body);
    Task<ApiResponse> DeleteAsync(string endpoint);
}