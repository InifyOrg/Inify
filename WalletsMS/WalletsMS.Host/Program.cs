using Microsoft.EntityFrameworkCore;
using UsersMS.Client;
using WalletsMS.Infrastructure;
using WalletsMS.Infrastructure.DataLayer;
using WalletsMS.Infrastructure.Domain.DbCtx;
using WalletsMS.Infrastructure.Services;

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

builder.Services.AddTransient<IWalletsDataLayer, WalletsDataLayer>();

builder.Services.AddScoped<IWalletsService, WalletsService>();

builder.Services.AddSingleton<IUsersMsClient, UsersMsClient>();

builder.Services.AddDbContext<WalletsMsDbContext>();

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<WalletsMsDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex) { throw; }
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
