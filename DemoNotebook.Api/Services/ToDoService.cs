
using AutoMapper;
using DemoNotebook.Api.Context;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing.Tree;

namespace DemoNotebook.Api.Services
{
    /// <summary>
    /// 待办事项的实现
    /// </summary>
    public class ToDoService : IToDoService
    {
        public  IMapper MyMapper { get; set; }
      
        private readonly IUnitOfWork _unitOfWork;

        public ToDoService(IUnitOfWork unitOfWork, IMapper myMapper)
        {
            _unitOfWork = unitOfWork;
            MyMapper = myMapper;
        }

        public async Task<ApiResponse> AddAsync(ToDoDTO  model)
        {
            try
            {
            var todoEntity = MyMapper.Map<ToDo>(model);
                await _unitOfWork.GetRepository<ToDo>().InsertAsync(todoEntity);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(model, true);
                }
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex )
            {

                return new ApiResponse(ex.Message);
            }
           
            
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                ToDo toDo = await _unitOfWork.GetRepository<ToDo>().GetFirstOrDefaultAsync(predicate:x=>x.Id.Equals(id) );
                   _unitOfWork.GetRepository<ToDo>().Delete(toDo);
                if (await _unitOfWork.SaveChangesAsync()>0)
                {
                    return  new ApiResponse("删除成功", true);

                }
               
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }

        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                
                var toDos = await _unitOfWork.GetRepository<ToDo>().GetPagedListAsync(predicate:
                    x=>string.IsNullOrWhiteSpace(parameter.Search)?true:x.Title.Equals(parameter.Search),
                pageIndex:parameter.PageIndex,
                pageSize:parameter.PageSize,
                orderBy:source=> source.OrderByDescending(x => x.CreateTime)
                ); 
                    return new ApiResponse(toDos, true);

            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var toDo = await _unitOfWork.GetRepository<ToDo>().GetFirstOrDefaultAsync(predicate:x=>x.Id.Equals(id));

                return new ApiResponse(toDo, true);

            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDTO model)
        {
            try
            {
            var todoEntity = MyMapper.Map<ToDo>(model);
                var todo = await _unitOfWork.GetRepository<ToDo>().GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(todoEntity.Id));
                todo.Title = todoEntity.Title;
                todo.Content = todoEntity.Content;
                todo.UpdateTime = DateTime.Now;
                todo.Status = todoEntity.Status;
              
                _unitOfWork.GetRepository<ToDo>().Update(todo); // Remove await here
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse($"更新{model.Title}成功", true);
                }

                return new ApiResponse($"数据{model.Title}更新失败");

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
