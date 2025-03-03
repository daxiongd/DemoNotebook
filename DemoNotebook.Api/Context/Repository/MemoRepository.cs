using Microsoft.EntityFrameworkCore;

namespace DemoNotebook.Api.Context.Repository
{

    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(MyContext MyContext) : base(MyContext)
        {
        }
    }
}
