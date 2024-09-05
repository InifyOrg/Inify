using BlockchainParsersMS.Client;
using TokensMs.Client;
using UsersMS.Client;
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

builder.Services.AddScoped<IUsersMsClient, UsersMsClient>();
builder.Services.AddScoped<IWalletsMsClient, WalletsMsClient>();
builder.Services.AddScoped<ITokensMsClient, TokensMsClient>();
builder.Services.AddScoped<IBlockchainParserClient, BlockchainParserClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
