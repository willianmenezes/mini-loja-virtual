using LojaVirtual.Application.Handlers.CategoriaHandler;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Editar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
using LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;
using LojaVirtual.Application.Handlers.PagamentoHandler;
using LojaVirtual.Application.Handlers.PedidoHandler;
using LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;
using LojaVirtual.Application.Handlers.PedidoHandler.FinalizarPedido;
using LojaVirtual.Application.Handlers.ProdutoHandler;
using LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Editar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Listar;
using LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;
using LojaVirtual.Application.Mappings;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.Integration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LojaVirtual.Application.RegisterServices;

public static class RegisterServicesExtensions
{
    public static IServiceCollection RegistrarServicosApplication(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CadastrarCategoriaRequest, BaseResponse>, CategoriaHandler>();
        services.AddTransient<IRequestHandler<ListarCategoriaRequest, BaseResponse>, CategoriaHandler>();
        services.AddTransient<IRequestHandler<ListarCategoriaPorIdRequest, BaseResponse>, CategoriaHandler>();
        services.AddTransient<IRequestHandler<EditarCategoriaRequest, BaseResponse>, CategoriaHandler>();
        
        services.AddTransient<IRequestHandler<CadastrarProdutoRequest, BaseResponse>, ProdutoHandler>();
        services.AddTransient<IRequestHandler<ListarProdutoRequest, BaseResponse>, ProdutoHandler>();
        services.AddTransient<IRequestHandler<ListarProdutoPorIdRequest, BaseResponse>, ProdutoHandler>();
        services.AddTransient<IRequestHandler<EditarProdutoRequest, BaseResponse>, ProdutoHandler>();
        services.AddTransient<IRequestHandler<MovimentarEstoqueRequest, bool>, ProdutoHandler>();
        
        services.AddTransient<IRequestHandler<AdicionarItemPedidoRequest, BaseResponse>, PedidoHandler>();
        services.AddTransient<IRequestHandler<CancelarPedidoRequest, BaseResponse>, PedidoHandler>();
        services.AddTransient<IRequestHandler<FinalizarPedidoRequest, BaseResponse>, PedidoHandler>();
        
        services.AddTransient<IRequestHandler<RealizarPagamentoRequest, BaseResponse>, PagamentoHandler>();
        
        services.AddAutoMapper(typeof(CategoriaProfile));
        return services;
    }
}