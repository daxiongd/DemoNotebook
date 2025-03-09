using DemoNotebook.Models;
using DemoNotebook.Shared.Contract;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Service
{
    public class MemoService : BaseService<MemoDTO>, IMemoService
    {
        private readonly IApiClient _restSharpApiClient;

        public MemoService(IApiClient restSharpApiClient) : base(restSharpApiClient, "MemoControl")
        {
            this._restSharpApiClient = restSharpApiClient;
        }
        public async Task<ApiResponse<PagedList<MemoDTO>>> GetAllFilterAsync(MyQueryParameter parameter)
        {
            string route = $"api/MemoControl/GetAll";
            return await _restSharpApiClient.GetAsync<PagedList<MemoDTO>>(route, parameter);
        }


        public async Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            //return await client.ExecuteAsync<SummaryDto>(request);

            string route = $"api/MemoControl/Summary";
            return await _restSharpApiClient.GetAsync<SummaryDto>(route);
        }
    }
}
