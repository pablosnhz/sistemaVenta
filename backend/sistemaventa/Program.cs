using Microsoft.EntityFrameworkCore;
using Pos.Model.Context;
using AutoMapper;
using Pos.Repository.Repository;
using Pos.Model.Model;
using Pos.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Pos.Service.Service;
using Pos.Service.Interface;
using FluentValidation;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sistemaventa.Middlewares;

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

// config de ASP.Net Identity
builder.Services.AddIdentity<Usuario, IdentityRole<int>>()
    .AddEntityFrameworkStores<PosContext>()
    .AddDefaultTokenProviders();

// configuracion de validaciones de los DTOs
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Pos.Dto"));

// configuracion JWT
var secretKey = builder.Configuration["Jwt:SecretKey"];
if(string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("La clave secreta JWT no esta configurada.");
}

var key = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt: Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// registramos el automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//registro de repositorios con sus interfaces
builder.Services.AddScoped<Rol_Repository>();

builder.Services.AddScoped<Categoria_Repository>();
builder.Services.AddScoped<Producto_Repository>();

builder.Services.AddScoped<INegocio_Repository ,Negocio_Repository>();
builder.Services.AddScoped<IDocumento_Repository, Documento_Repository>();
builder.Services.AddScoped<IUsuario_Repository, Usuario_Repository>();
builder.Services.AddScoped<IVenta_Repository, Venta_Repository>();

// registro de services con sus interfaces
builder.Services.AddScoped<Rol_Service>();
builder.Services.AddScoped<Categoria_Service>();
builder.Services.AddScoped<Producto_Service>();
builder.Services.AddScoped<IUsuario_Service, Usuario_Service>();
builder.Services.AddScoped<IDocumento_Service, DocumentoService>();
builder.Services.AddScoped<INegocio_Service, Negocio_Service>();
builder.Services.AddScoped<IVenta_Service, Venta_Service>();

// configuracion de los CORS para permitir solicitudes desde angular
builder.Services.AddCors(option =>
{
    option.AddPolicy("AccesoAngular", policy =>
    {
        // configuracion politica para permitir solicitudes desde el origen especificado
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// middleware de excepciones personalizadas
app.UseMiddleware<ExceptionMiddleware> ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// aplicamos las politicas de CORS
app.UseCors("AccesoAngular");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
