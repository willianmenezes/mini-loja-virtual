using LojaVirtual.Core.Domain;
using LojaVirtual.Domain.Enums;

namespace LojaVirtual.Domain.Entities;

public class Pedido : Entity
{
    public Guid UsuarioId { get; private set; }
    public decimal ValorTotal { get; private set; }
    public StatusPedido Status { get; private set; }
    private List<PedidoItem> _pedidoItems;
    public IEnumerable<PedidoItem> PedidoItens => _pedidoItems;
    public Pagamento Pagamento { get; private set; }

    public Pedido(Guid usuarioId)
    {
        UsuarioId = usuarioId;
        _pedidoItems = new List<PedidoItem>();
        Status = StatusPedido.Carrinho;
    }

    public void CalcularValorTotal()
    {
        ValorTotal = _pedidoItems.Sum(pedidoItem => pedidoItem.CalcularValor());
    }

    public void IniciarPedido() => Status = StatusPedido.Iniciado;
    public void RetornarPedidoCarrinho() => Status = StatusPedido.Carrinho;

    public void AdicionarItemNoPedido(PedidoItem pedidoItem)
    {
        pedidoItem.VincularPedido(Id);

        if (ExistePedidoItem(pedidoItem)
                is var itemEncontrado && itemEncontrado != null)
        {
            itemEncontrado.AdicionarQuantidade(pedidoItem.Quantidade);
            pedidoItem = itemEncontrado;
        }
        else
        {
            _pedidoItems.Add(pedidoItem);
        }

        CalcularValorTotal();
    }

    public void RemoverItem(PedidoItem pedidoItem)
    {
        if (ExistePedidoItem(pedidoItem)
                is var itemEncontrado && itemEncontrado == null)
        {
            throw new DomainException("O item nao foi encontrado no pedido. Item invalido.");
        }

        _pedidoItems.Remove(itemEncontrado);
        CalcularValorTotal();
    }

    public void AtualizarQuantidadeItem(PedidoItem item, int novaQuantidade)
    {
        if (ExistePedidoItem(item)
                is var itemEncontrado && itemEncontrado == null)
        {
            throw new DomainException("O item nao foi encontrado no pedido. Item invalido.");
        }

        itemEncontrado.AtualizarQuantidade(novaQuantidade);
        CalcularValorTotal();
    }

    private PedidoItem? ExistePedidoItem(PedidoItem item)
    {
        return _pedidoItems.FirstOrDefault(pedidoItem => pedidoItem.ProdutoId == item.ProdutoId);
    }

    public bool ExistePedidoItem(Guid produtoId)
    {
        return _pedidoItems.Any(pedidoItem => pedidoItem.ProdutoId == produtoId);
    }

    public override void Validar()
    {
    }
}