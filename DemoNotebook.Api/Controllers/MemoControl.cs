using DemoNotebook.Api.Context;
using DemoNotebook.Api.Services;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace DemoNotebook.Api.MemoControl
{
    /// <summary>
    /// 代办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class MemoControl : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private readonly IMemoService _memoService;

        public MemoControl(IToDoService ToDoService,IMemoService MemoService)
        {
            _toDoService = ToDoService;
            _memoService = MemoService;
        }
        [HttpGet]
        public async Task<ApiResponse> Get(int id)=> await _memoService.GetSingleAsync(id);
        [HttpPost]

        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await _memoService.GetAllAsync(parameter);
        [HttpPost]

        public async Task<ApiResponse> Add([FromBody] MemoDTO model) => await _memoService.AddAsync(model);
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDTO model) => await _memoService.UpdateAsync(model);
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)=> await _memoService.DeleteAsync(id);


    }
}
