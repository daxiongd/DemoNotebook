using Microsoft.EntityFrameworkCore;

namespace DemoNotebook.Api.Context.Repository
{
    
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(MyContext MyContext) : base(MyContext)
        {
        }
    }
}
