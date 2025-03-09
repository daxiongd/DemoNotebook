using DemoNotebook.Models;
using DemoNotebook.Shared.Contract;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Service
{
    public class ToDoService : BaseService<ToDoDTO>, IToDoService
    {
        private readonly IApiClient _restSharpApiClient;

        public ToDoService(IApiClient restSharpApiClient) : base(restSharpApiClient, "ToDoControl")
        {
            this._restSharpApiClient = restSharpApiClient;

        }

        public async Task<ApiResponse<PagedList<ToDoDTO>>> GetAllFilterAsync(MyQueryParameter parameter)
        {
            string route = $"api/ToDoControl/GetAll";
            return await _restSharpApiClient.GetAsync<PagedList<ToDoDTO>>(route,parameter);
        }


        public async Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            //return await client.ExecuteAsync<SummaryDto>(request);
          
            string route = $"api/ToDoControl/Summary";
            return await _restSharpApiClient.GetAsync<SummaryDto>(route);
        }
    }
}
