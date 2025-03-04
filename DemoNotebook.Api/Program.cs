using DemoNotebook.Api;
using DemoNotebook.Api.Context;
using DemoNotebook.Api.Context.Repository;
using DemoNotebook.Api.Extension;
using DemoNotebook.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.GetConnectionString("ToDoConnection");
//配置数据库sqlite依赖注入
builder.Services.AddDbContext<MyContext>(option =>
{
    option.UseSqlite(configuration);
}).AddUnitOfWork<MyContext>()
.AddCustomRepository<ToDo,ToDoRepository>()
.AddCustomRepository<Memo, MemoRepository>()
.AddCustomRepository<User, UserRepository>();
//通过 AddTransient，你将 IToDoService 注册为 ToDoService，这意味着当你在代码中注入 IToDoService 时，实际上会使用 ToDoService 的实例。
builder.Services.AddTransient <IToDoService,ToDoService>();
builder.Services.AddTransient <IMemoService,MemoService>();
builder.Services.AddTransient <ILoginService,LoginService>();
//注册automapper
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
