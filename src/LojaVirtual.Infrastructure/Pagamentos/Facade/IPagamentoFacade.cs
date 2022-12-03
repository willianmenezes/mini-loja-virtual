using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Infrastructure.Pagamentos.Facade;

public interface IPagamentoFacade
{
    Task<Transacao> RealizarPagamentoAsync(Guid pedidoId, decimal valorPedido, Pagamento pagamento);
}