using Microsoft.EntityFrameworkCore;

namespace DemoNotebook.Api.Context
{
    public class MyContext:DbContext
    {
        DbSet<ToDo> ToDos { get; set; }
        DbSet<Memo> Memo { get; set; }
        DbSet<User> User { get; set; }
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
            
        }
    }
}
