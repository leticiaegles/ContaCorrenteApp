using ContaCorrenteApi.Services;
using ContaCorrentLibrary.DataAcess;
using ContaCorrentLibrary.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ISqlDataAcess, SqlDataAcess>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IMovementService, MovementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
