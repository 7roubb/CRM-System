using CRM.Data;
using CRM.Exceptions;
using CRM.Services;
using CRM.Services.IServices;
using CRM.Uitlity.DBInitlizer;
using CRM.Utility.DBInitializer;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDBInitlizer, DBInitlizerService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();
using var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitlizer>();
await dbInitializer.initlizerAsync();
TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

app.MapControllers();

app.Run();
