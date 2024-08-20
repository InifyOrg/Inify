using BlockchainParsersMS.Infrastructure;
using BlockchainParsersMS.Infrastructure.Services;
using TokensMs.Client;
using WalletsMS.Client;

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


builder.Services.AddScoped<IWeb3Service, Web3Service>();
builder.Services.AddScoped<IBlockchainParserService, BlockchainParserService>();
builder.Services.AddScoped<ITokensMsClient, TokensMsClient>();
builder.Services.AddScoped<IWalletsMsClient, WalletsMsClient>();
builder.Services.AddScoped<ICoinMarketCapService, CoinMarketCapService>();


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
