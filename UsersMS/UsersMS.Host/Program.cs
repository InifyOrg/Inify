using UsersMS.Infrastructure.DataLayer;
using UsersMS.Infrastructure.Services;
using UsersMS.Infrastructure;
using UsersMS.Client;
using UsersMS.Infrastructure.Domain.DbCtx;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<IUsersDataLayer, UsersDataLayer>();

builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddDbContext<UserMsDbContext>();


var app = builder.Build();

// Apply migrations

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<UserMsDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex) { throw; }
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseMiddleware<UsersMsAuthMiddleware>();

app.MapControllers();

app.Run();

