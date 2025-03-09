using DemoNotebook.Shared.Contract;
using DemoNotebook.Shared.Parameters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        
        private readonly string serviceName;
        private readonly IApiClient _restSharpApiClient;

        public BaseService(IApiClient restSharpApiClient, string serviceName)
        {
            
            this.serviceName = serviceName;
            this._restSharpApiClient = restSharpApiClient;
        }


        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            RestRequest request = new RestRequest($"api/{serviceName}/Add").AddJsonBody(entity);
            string route = $"api/{serviceName}/Add";
            return await _restSharpApiClient.GetAsync<TEntity>(route, entity);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
          
            string route = $"api/{serviceName}/Delete?id={id}";
            return await _restSharpApiClient.DeleteAsync(route);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(MyQueryParameter parameter)
        {

            string route = $"api/{serviceName}/GetAll";
            return await _restSharpApiClient.GetAsync<PagedList<TEntity>>(route, parameter);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
           
            string route = $"api/{serviceName}/Get?id={id}";
            return await _restSharpApiClient.GetAsync<TEntity>(route);
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            
            string route = $"api/{serviceName}/Update";
            return await _restSharpApiClient.PostAsync<TEntity>(route,entity);
        }
    }
}
