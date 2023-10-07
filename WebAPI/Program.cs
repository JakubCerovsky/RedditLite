using Application.DAOInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using FileData;
using FileData.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDAO, UserFileDAO>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IPostDAO, PostFileDAO>();
builder.Services.AddScoped<IPostLogic, PostLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();