using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface IPedidoRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task<Pedido?> BuscarPedidoIniciadoPorIdUsuario(Guid usuarioId);
    IQueryable<Pedido> ObterPedidosPorUsuario(Guid usuarioId);
    void Adicionar(Pedido pedido);
    Task<Pedido?> BuscarPorId(Guid id);
}