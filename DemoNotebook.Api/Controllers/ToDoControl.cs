using DemoNotebook.Api.Context;
using DemoNotebook.Api.Servies;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace DemoNotebook.Api.Controllers
{
    /// <summary>
    /// 代办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class ToDoControl:ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoControl(IToDoService ToDoService)
        {
            _toDoService = ToDoService;
        }
        [HttpGet]
        public async Task<ApiResponse> Get(int id)=> await _toDoService.GetSingleAsync(id);
        [HttpGet]

        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter)=> await _toDoService.GetAllAsync(parameter);
        [HttpPost]

        public async Task<ApiResponse> Add([FromBody] ToDoDTO todo) => await _toDoService.AddAsync(todo);
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDTO todo)=> await _toDoService.UpdateAsync(todo);
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)=> await _toDoService.DeleteAsync(id);


    }
}
