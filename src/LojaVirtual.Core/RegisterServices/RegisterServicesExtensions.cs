using LojaVirtual.Core.NotificationError;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LojaVirtual.Core.RegisterServices;

public static class RegisterServicesExtensions
{
    public static IServiceCollection RegistrarServicosCore(this IServiceCollection services)
    {
        services.AddMediatR(typeof(NotificacaoErro).Assembly);
        services.AddScoped<INotificationHandler<NotificacaoErro>, NotificacaoErroHandler>();
        return services;
    }
}