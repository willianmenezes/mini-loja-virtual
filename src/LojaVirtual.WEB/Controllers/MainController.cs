using LojaVirtual.Core.NotificationError;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WEB.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected readonly IMediator Mediator;
    protected readonly NotificacaoErroHandler NotificacaoErroHandler;
    
    protected MainController(IMediator mediator, NotificacaoErroHandler notificacaoErroHandler)
    {
        Mediator = mediator;
        NotificacaoErroHandler = notificacaoErroHandler;
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