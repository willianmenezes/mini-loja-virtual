using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WEB.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : MainController
{
    public CategoriasController(IMediator mediator, INotificationHandler<NotificacaoErro> notificationHandler) : base(mediator,
        notificationHandler)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarCategoria([FromBody] CadastrarCategoriaRequest request)
    {
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
}