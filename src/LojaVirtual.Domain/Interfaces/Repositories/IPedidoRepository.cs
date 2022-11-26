using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface IPedidoRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task<Pedido?> BuscarPedidoCarrinhoPorIdUsuarioAsync(Guid usuarioId);
    IQueryable<Pedido> ObterPedidosPorUsuario(Guid usuarioId);
    Task AdicionarAsync(Pedido pedido);
    Task<Pedido?> BuscarPorIdAsync(Guid id);
    Task AdicionarItemAsync(PedidoItem pedidoItem);
}