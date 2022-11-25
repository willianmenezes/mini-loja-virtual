using LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WEB.Controllers;

[Route("[controller]")]
public class PedidosController : MainController
{
    public PedidosController(IMediator mediator, INotificationHandler<NotificacaoErro> notificationHandler) : base(mediator, notificationHandler)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> AdicionarPedidoAsync([FromBody] AdicionarItemPedidoRequest request)
    {
        request.UsuarioId = Guid.Parse("364d41a8-a916-4b59-8d31-bfa1b74de6e7");
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
}