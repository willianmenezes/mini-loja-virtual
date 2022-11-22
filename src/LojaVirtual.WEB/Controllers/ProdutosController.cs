using LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Listar;
using LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;
using LojaVirtual.Core.NotificationError;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.WEB.Controllers;

[Route("[controller]")]
public class ProdutosController : MainController
{
    public ProdutosController(
        IMediator mediator,
        INotificationHandler<NotificacaoErro> notificationHandler) : base(mediator, notificationHandler)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CadastrarProdutoAsync([FromBody] CadastrarProdutoRequest request)
    {
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }

    [HttpGet]
    public async Task<IActionResult> ListarCategoriaAsync([FromQuery] ListarProdutoRequest categoriaRequest)
    {
        var resultado = await Mediator.Send(categoriaRequest);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ListarProdutoPorIdAsync(Guid id)
    {
        var resultado = await Mediator.Send(new ListarProdutoPorIdRequest(id));

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
}