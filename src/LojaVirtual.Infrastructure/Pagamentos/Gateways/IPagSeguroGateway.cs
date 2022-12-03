using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Infrastructure.Pagamentos.Gateways;

public interface IPagSeguroGateway
{
    Task<PagSeguroGatewayResponse> EfetivarTransacao(Guid pedidoId, decimal valorPedido, Pagamento pagamento);
}