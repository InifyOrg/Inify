using UsersMS.Client;
using WalletsMS.Infrastructure;
using WalletsMS.Infrastructure.DataLayer;
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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
