using DemoNotebook.Api;
using DemoNotebook.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace DemoNotebook.Api.Context.Repository
{
    public class ToDoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public ToDoRepository(MyContext MyContext) : base(MyContext)
        {
        }
    }
}


