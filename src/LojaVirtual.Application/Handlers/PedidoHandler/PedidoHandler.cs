using AutoMapper;
using LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.PedidoHandler;

public class PedidoHandler :
    BaseHandler,
    IRequestHandler<AdicionarItemPedidoRequest, BaseResponse>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoHandler(IMediator mediator, IMapper mapper, IProdutoRepository produtoRepository,
        IPedidoRepository pedidoRepository) : base(mediator, mapper)
    {
        _produtoRepository = produtoRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<BaseResponse> Handle(AdicionarItemPedidoRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new AdicionarPedidoRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var pedidoIniciado = await _pedidoRepository.BuscarPedidoIniciadoPorIdUsuarioAsync(request.UsuarioId);
        var produto = await _produtoRepository.BuscarPorIdAsync(request.ProdutoId);

        if (produto is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(AdicionarItemPedidoRequest)}",
                "Produto não encontrado."));
            return BaseResponse.Erro();
        }

        var pedidoItem = new PedidoItem(request.ProdutoId, request.Quantidade, produto.Valor, produto.Nome);

        if (pedidoIniciado is null)
        {
            var pedido = new Pedido(request.UsuarioId);
            pedido.AdicionarItemNoPedido(pedidoItem);
            await _pedidoRepository.AdicionarAsync(pedido);
        }
        else
        {
            var pedidoItemExistente = pedidoIniciado.ExistePedidoItem(pedidoItem.ProdutoId);
            pedidoIniciado.AdicionarItemNoPedido(pedidoItem);

            if (!pedidoItemExistente)
            {
                await _pedidoRepository.AdicionarItemAsync(pedidoItem);
            }
        }

        await _pedidoRepository.UnityOfWork.SalvarAlteracoesAsync();
        return BaseResponse.Sucesso();
    }
}