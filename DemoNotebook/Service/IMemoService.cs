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
    public interface IMemoService : IBaseService<MemoDTO>
    {
        Task<ApiResponse<PagedList<MemoDTO>>> GetAllFilterAsync(MyQueryParameter parameter);

        Task<ApiResponse<SummaryDto>> SummaryAsync();
    }
}
