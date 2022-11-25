using LojaVirtual.Core.DTOs;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;
using LojaVirtual.Infrastructure.DTOs;
using LojaVirtual.Infrastructure.Repositories;
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
        services.AddScoped(typeof(IPaginacao<>), typeof(Paginacao<>));
        return services;
    }
}