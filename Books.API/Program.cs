using Books.API.Contexts;
using Books.API.Interfaces.Repositories;
using Books.API.Repositories;
using Microsoft.EntityFrameworkCore;

// ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<BookContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:Conn"]);
});
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();