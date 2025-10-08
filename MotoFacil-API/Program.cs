using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MotoFacilAPI.Infrastructure.Persistence;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Repositories;
using MotoFacilAPI.Application.Interfaces;
using MotoFacilAPI.Application.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Conexão Oracle definida em appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

// Repositórios (Domain/Infra)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();

// Serviços de Aplicação (Application)
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

// Controllers + Enum como string no JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// CORS liberado para dev
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Configuração Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoFacil API",
        Version = "v1",
        Description = "API para gerenciamento de usuários, motos e serviços no sistema MotoFácil"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// Pipeline
var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();