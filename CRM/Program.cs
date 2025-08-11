using CRM.Data;
using CRM.Services;
using CRM.Services.IServices;
using CRM.Uitlity.DBInitlizer;
using CRM.Utility.DBInitializer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDBInitlizer, DBInitlizerService>();

// تسجيل DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// تسجيل الخدمات
builder.Services.AddScoped<IUserService, UserService>();

// تسجيل باقي الخدمات
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<CRM.Middleware.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
using var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitlizer>();
await dbInitializer.initlizerAsync();


app.MapControllers();

app.Run();
