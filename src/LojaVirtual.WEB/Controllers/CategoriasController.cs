using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
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
    public async Task<IActionResult> CadastrarCategoriaAsync([FromBody] CadastrarCategoriaRequest request)
    {
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListarCategoriaAsync([FromQuery] ListarCategoriaRequest request)
    {
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
}