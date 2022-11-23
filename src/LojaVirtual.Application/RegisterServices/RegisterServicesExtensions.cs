using LojaVirtual.Application.Handlers.CategoriaHandler;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Editar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
using LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;
using LojaVirtual.Application.Handlers.ProdutoHandler;
using LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Editar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Listar;
using LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;
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
        services.AddScoped<IRequestHandler<ListarCategoriaRequest, BaseResponse>, CategoriaHandler>();
        services.AddScoped<IRequestHandler<ListarCategoriaPorIdRequest, BaseResponse>, CategoriaHandler>();
        services.AddScoped<IRequestHandler<EditarCategoriaRequest, BaseResponse>, CategoriaHandler>();
        
        services.AddScoped<IRequestHandler<CadastrarProdutoRequest, BaseResponse>, ProdutoHandler>();
        services.AddScoped<IRequestHandler<ListarProdutoRequest, BaseResponse>, ProdutoHandler>();
        services.AddScoped<IRequestHandler<ListarProdutoPorIdRequest, BaseResponse>, ProdutoHandler>();
        services.AddScoped<IRequestHandler<EditarProdutoRequest, BaseResponse>, ProdutoHandler>();
        
        services.AddAutoMapper(typeof(CategoriaProfile));
        return services;
    }
}