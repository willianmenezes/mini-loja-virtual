using AutoMapper;
using LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;
using LojaVirtual.Application.Handlers.PedidoHandler.BuscarCarrinho;
using LojaVirtual.Application.Handlers.PedidoHandler.FinalizarPedido;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WEB.Controllers;

[Route("[controller]")]
public class PedidosController : MainController
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;

    public PedidosController(IMediator mediator, INotificationHandler<NotificacaoErro> notificationHandler,
        IPedidoRepository pedidoRepository, IMapper mapper) : base(mediator, notificationHandler)
    {
        _pedidoRepository = pedidoRepository;
        _mapper = mapper;
    }

    [HttpPost("adicionar-item-carrinho")]
    public async Task<IActionResult> ItemCarrinhoAsync([FromBody] AdicionarItemPedidoRequest request)
    {
        request.UsuarioId = Guid.Parse("364d41a8-a916-4b59-8d31-bfa1b74de6e7");
        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }

    [HttpGet("carrinho")]
    public async Task<IActionResult> BuscarCarrinhoAsync([FromRoute] Guid usuarioId)
    {
        usuarioId = Guid.Parse("364d41a8-a916-4b59-8d31-bfa1b74de6e7");
        var pedidoEmAndamento = await _pedidoRepository.BuscarPedidoCarrinhoPorIdUsuarioAsync(usuarioId);

        if (pedidoEmAndamento is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(BuscarCarrinhoAsync)}",
                "Carrinho vazio."));
            return BadRequest(BaseResponse.Erro(ObterErros()));
        }

        return Ok(_mapper.Map<PedidoResponse>(pedidoEmAndamento));
    }
    
    [HttpPost("fechar-pedido")]
    public async Task<IActionResult> FecharPedidoAsync([FromBody] FinalizarPedidoRequest request)
    {
        request.UsuarioId = Guid.Parse("364d41a8-a916-4b59-8d31-bfa1b74de6e7");

        var resultado = await Mediator.Send(request);

        if (ProcessoInvalido())
            return BadRequest(BaseResponse.Erro(ObterErros()));

        return Ok(resultado);
    }
}