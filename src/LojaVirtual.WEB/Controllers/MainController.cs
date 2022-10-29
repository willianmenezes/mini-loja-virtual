using LojaVirtual.Core.NotificationError;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WEB.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected readonly IMediator Mediator;
    protected readonly NotificacaoErroHandler NotificacaoErroHandler;
    
    protected MainController(IMediator mediator, INotificationHandler<NotificacaoErro> notificationHandler)
    {
        Mediator = mediator;
        NotificacaoErroHandler =  (NotificacaoErroHandler)notificationHandler;;
    }
    
    protected bool ProcessoInvalido()
    {
        return NotificacaoErroHandler.ExisteErros();
    }

    protected IEnumerable<NotificacaoErro> ObterErros()
    {
        return NotificacaoErroHandler.ObterErros();
    }
}