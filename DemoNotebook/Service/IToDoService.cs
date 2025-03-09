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
    public interface IToDoService : IBaseService<ToDoDTO>
    {
        Task<ApiResponse<PagedList<ToDoDTO>>> GetAllFilterAsync(MyQueryParameter parameter);

        Task<ApiResponse<SummaryDto>> SummaryAsync();
    }
}
