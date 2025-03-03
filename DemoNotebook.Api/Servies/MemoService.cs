
using System.Reflection.Metadata;
using AutoMapper;
using DemoNotebook.Api.Context;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using Microsoft.AspNetCore.Routing.Tree;

namespace DemoNotebook.Api.Servies
{
    /// <summary>
    /// 备忘录的实现
    /// </summary>
    public class MemoService : IMemoService
    {
        public  IMapper MyMapper { get; set; }
      
        private readonly IUnitOfWork _unitOfWork;

        public MemoService(IUnitOfWork unitOfWork, IMapper myMapper)
        {
            _unitOfWork = unitOfWork;
            MyMapper = myMapper;
        }

        public async Task<ApiResponse> AddAsync(MemoDTO  model)
        {
            try
            {
            var Entity = MyMapper.Map<Memo>(model);
                await _unitOfWork.GetRepository<Memo>().InsertAsync(Entity);
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
                    await _unitOfWork.GetRepository<Memo>().GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                    _unitOfWork.GetRepository<Memo>().Delete(id);
                if (await _unitOfWork.SaveChangesAsync()>0)
                {
                    return new ApiResponse("删除成功", true);

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
                var memos = await _unitOfWork.GetRepository<ToDo>().GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(parameter.Search) || x.Title.Equals(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(x => x.CreateTime)
                );
              
                return new ApiResponse(memos, true);

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
                var entity = await _unitOfWork.GetRepository<Memo>().GetFirstOrDefaultAsync(predicate:x=>x.Id.Equals(id));

                return new ApiResponse(entity, true);

            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDTO model)
        {
            try
            {
            var entity = MyMapper.Map<Memo>(model);
                var memo = await _unitOfWork.GetRepository<Memo>().GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(entity.Id));
                memo.Title = entity.Title;
                memo.Content = entity.Content;
                memo.UpdateTime = DateTime.Now;
              
                _unitOfWork.GetRepository<Memo>().Update(memo); // Remove await here
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
