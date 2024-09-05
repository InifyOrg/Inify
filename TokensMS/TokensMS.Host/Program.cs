using Microsoft.EntityFrameworkCore;
using TokensMS.Infrastructure;
using TokensMS.Infrastructure.DataLayer;
using TokensMS.Infrastructure.Domain.DbCtx;
using TokensMS.Infrastructure.Services;

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

builder.Services.AddScoped<ITokensService, TokensService>();
builder.Services.AddScoped<IWeb3Service, Web3Service>();
builder.Services.AddScoped<ICoinMarketCapService, CoinMarketCapService>();
builder.Services.AddTransient<ITokensDataLayer, TokensDataLayer>();

builder.Services.AddDbContext<TokensMsDbContext>();
var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<TokensMsDbContext>();
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
