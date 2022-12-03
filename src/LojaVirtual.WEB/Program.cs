using LojaVirtual.Application.RegisterServices;
using LojaVirtual.Core.RegisterServices;
using LojaVirtual.Infrastructure.DTOs;
using LojaVirtual.Infrastructure.RegisterServices;
using LojaVirtual.WEB.Configurations;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegistrarServicosCore();
builder.Services.RegistrarServicosApplication();
builder.Services.RegistrarServicosInfrastructure(builder.Configuration);

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddHttpClient();
builder.Services.Configure<PagamentoConfiguration>(builder.Configuration.GetSection(nameof(PagamentoConfiguration)));

var app = builder.Build();
app.UseMiddleware<MainErrorHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();