using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Editar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
using LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WEB.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : MainController
{
    public CategoriasController(IMediator mediator, INotificationHandler<NotificacaoErro> notificationHandler) : base(
        mediator,
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
    public async Task<IActionResult> ListarCategoriaAsync([FromQuery] ListarCategoriaRequest categoriaRequest)
    {
        var resultado = await Mediator.Send(categoriaRequest);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ListarCategoriaAsync(Guid id)
    {
        var resultado = await Mediator.Send(new ListarCategoriaPorIdRequest(id));

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> EditarCategoriaAsync(Guid id, [FromBody] EditarCategoriaRequest request)
    {
        if (id != request.Id)
            return BadRequest(BaseResponse.Erro(new List<NotificacaoErro>
            {
                new(nameof(EditarCategoriaRequest), "Erro ao editar categoria")
            }));
        
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
}