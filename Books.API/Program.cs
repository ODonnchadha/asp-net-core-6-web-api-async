using Books.API.Contexts;
using Books.API.Interfaces.Repositories;
using Books.API.Interfaces.Services;
using Books.API.Repositories;
using Books.API.Services;
using Microsoft.EntityFrameworkCore;

// ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<BookContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:Conn"]);
});
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookCoverService, BookCoverService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();