using LojaVirtual.Core.Domain;
using LojaVirtual.Domain.Enums;

namespace LojaVirtual.Domain.Entities;

public class Transacao : Entity
{
    public Guid PagamentoId { get; private set; }
    public decimal Total { get; private set; }
    public string PagamentoGatewayId { get; private set; }
    public StatusTransacao Status { get; private set; }

    // EF relation
    public Pagamento Pagamento { get; set; }

    public Transacao(Guid pagamentoId, decimal total, string pagamentoGatewayId)
    {
        PagamentoId = pagamentoId;
        Total = total;
        PagamentoGatewayId = pagamentoGatewayId;
    }

    public void TransacaoPaga() => Status = StatusTransacao.Pago;
    public void TransacaoRecusada() => Status = StatusTransacao.Recusada;

    public override void Validar()
    {
        throw new NotImplementedException();
    }
}