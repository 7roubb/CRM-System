using CRM.Data;
using CRM.Services;
using CRM.Services.IServices;
using CRM.Utility.DBInitializer;
using Mapster;
using CRM.Uitlity.DBInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRM.Middleware;

var builder = WebApplication.CreateBuilder(args);


// DB Initializer
builder.Services.AddScoped<IDBInitializer, DBInitializerService>();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Settings from appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Application Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- Build App --------------------
var app = builder.Build();

// -------------------- Middleware --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler(app.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<CustomExceptionMiddleware>();

// -------------------- DB Initialization --------------------
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
    await dbInitializer.initlizerAsync();
}

// -------------------- Mapster Global Settings --------------------
TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

// -------------------- Map Controllers --------------------
app.MapControllers();

// -------------------- Run App --------------------
app.Run();
