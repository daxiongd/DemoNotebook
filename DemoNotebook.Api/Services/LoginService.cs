using AutoMapper;
using DemoNotebook.Api;
using DemoNotebook.Api.Context;
using DemoNotebook.Api.Services;
using DemoNotebook.Shared;
using DemoNotebook.Shared.DTO;
using MD5Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoNotebook.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> LoginAsync(string Account, string Password)
        {
            try
            {
                Password = Password.GetMD5();

                var model = await work.GetRepository<User>().GetFirstOrDefaultAsync(predicate:
                    x => (x.Account.Equals(Account)) &&
                    (x.PassWord.Equals(Password)));

                if (model == null)
                    return new ApiResponse("账号或密码错误,请重试！");

                return new ApiResponse( new UserDto()
                {
                    Account = model.Account,
                    UserName = model.UserName,
                    Id = model.Id
                },true);
            }
            catch (Exception ex)
            {
                return new ApiResponse( "登录失败！",false);
            }
        }

        public async Task<ApiResponse> Resgiter(UserDto user)
        {
            try
            {
                var model = mapper.Map<User>(user);
                var repository = work.GetRepository<User>();
                var userModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Account.Equals(model.Account));

                if (userModel != null)
                    return new ApiResponse($"当前账号:{model.Account}已存在,请重新注册！");

                model.CreateTime = DateTime.Now;
                model.PassWord = model.PassWord.GetMD5();
                await repository.InsertAsync(model);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse( model,true);

                return new ApiResponse("注册失败,请稍后重试！");
            }
            catch (Exception ex)
            {
                return new ApiResponse("注册账号失败！");
            }
        }
    }
}
