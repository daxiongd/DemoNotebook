using AutoMapper;
using DemoNotebook.Api.Context;
using DemoNotebook.Shared.DTO;

namespace DemoNotebook.Api.Extension
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDo,ToDoDTO>().ReverseMap();
            CreateMap<Memo,MemoDTO>().ReverseMap();


        }
    }
}
