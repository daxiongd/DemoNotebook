using DemoNotebook.Api.Services;
using DemoNotebook.Shared.Contract;
using DemoNotebook.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoNotebook.Api.Services
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string Account, string Password);

        Task<ApiResponse> Resgiter(UserDto user);
    }
}
