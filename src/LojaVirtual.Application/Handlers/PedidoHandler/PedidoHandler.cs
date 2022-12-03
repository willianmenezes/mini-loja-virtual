using AutoMapper;
using LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;
using LojaVirtual.Application.Handlers.PedidoHandler.FinalizarPedido;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.Handlers;
using LojaVirtual.Core.Integration;
using LojaVirtual.Core.NotificationError;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.PedidoHandler;

public class PedidoHandler :
    BaseHandler,
    IRequestHandler<AdicionarItemPedidoRequest, BaseResponse>,
    IRequestHandler<FinalizarPedidoRequest, BaseResponse>,
    IRequestHandler<CancelarPedidoRequest, BaseResponse>
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

        var pedidoIniciado = await _pedidoRepository.BuscarPedidoCarrinhoPorIdUsuarioAsync(request.UsuarioId);
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

    public async Task<BaseResponse> Handle(FinalizarPedidoRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new FinalizarPedidoRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();
        
        var pedidoCarrinho = await _pedidoRepository.BuscarPedidoCarrinhoPorIdUsuarioAsync(request.UsuarioId);
        
        if (pedidoCarrinho is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(FinalizarPedidoRequest)}",
                "Carrinho de compras inválido."), cancellationToken);
            return BaseResponse.Erro();
        }
        pedidoCarrinho.IniciarPedido();
        await _pedidoRepository.UnityOfWork.SalvarAlteracoesAsync();
        
        await Mediator.Publish(new FinalizarPedidoEvent
        {
            Total = pedidoCarrinho.ValorTotal,
            ExpiracaoCartao = request.ExpiracaoCartao,
            NomeCartao = request.NomeCartao,
            NumeroCartao = request.NumeroCartao,
            CodigoVerificadoCartao = request.CodigoVerificadoCartao,
            UsuarioId = request.UsuarioId,
            PedidoId = pedidoCarrinho.Id,
            Itens = pedidoCarrinho.PedidoItens.Select(x => new Item()
            {
                Id = x.ProdutoId,
                Quantidade = x.Quantidade
            })
        }, cancellationToken);

        return BaseResponse.Sucesso();
    }

    public async Task<BaseResponse> Handle(CancelarPedidoRequest request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.BuscarPorIdAsync(request.PedidoId);

        if (pedido is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(CancelarPedidoRequest)}",
                "Pedido não encontrado."));
            return BaseResponse.Erro();
        }

        pedido.RetornarPedidoCarrinho();
        await _pedidoRepository.UnityOfWork.SalvarAlteracoesAsync();
        
        return BaseResponse.Sucesso();
    }
}