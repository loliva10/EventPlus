using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsuarioContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "API de Eventos",
    Description = "Aplicação para gerenciamento de Eventos",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "Luis Oliva",
        Url = new Uri("https://www.linkedin.com/in/luis-fernando-de-oliva-20a884383/")
    },
    License = new OpenApiLicense
    {
        Name = "Exemplo de licensa",
        Url = new Uri("http://example.com/license")
    }
});

// Usando a autenticação do swagger
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insira o token JWT: "
});

options.AddSecurityRequirement(document => new OpenApiSecurityRequirement 
{
    [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
});
});

builder.Services.AddOpenApi();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
