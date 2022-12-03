using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.Integration;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;
using LojaVirtual.Infrastructure.DTOs;
using LojaVirtual.Infrastructure.HttpClients.Pagamento;
using LojaVirtual.Infrastructure.Pagamentos.Facade;
using LojaVirtual.Infrastructure.Pagamentos.Gateways;
using LojaVirtual.Infrastructure.Pagamentos.Handlers;
using LojaVirtual.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LojaVirtual.Infrastructure.RegisterServices;

public static class RegisterServicesExtensions
{
    public static IServiceCollection RegistrarServicosInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("LojaVirtualConnection"),
                x => x.MigrationsHistoryTable("Historico_Migracoes_EF")));

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
        services.AddScoped(typeof(IPaginacao<>), typeof(Paginacao<>));
        
        services.AddScoped<IRequestHandler<RealizarPagamentoRequest, BaseResponse>, PagamentoHandler>();
        
        services.AddScoped<IPagSeguroGateway, PagSeguroGateway>();
        services.AddScoped<IPagamentoFacade, PagamentoFacade>();
        services.AddScoped<IHttpClientPagamentoService, HttpClientPagamentoService>();
        
        return services;
    }
}