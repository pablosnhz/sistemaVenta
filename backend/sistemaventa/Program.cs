using Microsoft.EntityFrameworkCore;
using Pos.Model.Context;
using AutoMapper;
using Pos.Repository.Repository;
using Pos.Model.Model;
using Pos.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Obtenemos y validamos la cadena de conexion de la base de datos
var ConnectionStrings = builder.Configuration.GetConnectionString("Connection");
if (string.IsNullOrEmpty(ConnectionStrings))
{
    throw new InvalidOperationException("La cadena de conexion 'Connection' no esta configurada");
}
builder.Services.AddDbContext<PosContext>(options =>
{
    options.UseNpgsql(ConnectionStrings);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// registramos el automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//registro de repositorios con sus interfaces
//builder.Services.AddScoped<Rol_Repository>();
builder.Services.AddScoped<IRepository<Rol>, Rol_Repository>();


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
