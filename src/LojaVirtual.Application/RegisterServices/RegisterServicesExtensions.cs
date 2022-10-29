﻿using LojaVirtual.Application.Handlers.CategoriaHandler;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Mappings;
using LojaVirtual.Core.DTOs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LojaVirtual.Application.RegisterServices;

public static class RegisterServicesExtensions
{
    public static IServiceCollection RegistrarServicosApplication(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CadastrarCategoriaRequest, BaseResponse>, CategoriaHandler>();
        services.AddAutoMapper(typeof(CategoriaProfile));
        return services;
    }
}