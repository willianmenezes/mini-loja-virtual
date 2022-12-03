using LojaVirtual.Domain.Entities;
using LojaVirtual.Infrastructure.Pagamentos.Gateways;

namespace LojaVirtual.Infrastructure.Pagamentos.Facade;

public class PagamentoFacade : IPagamentoFacade
{
    private readonly IPagSeguroGateway _pagSeguroGateway;

    public PagamentoFacade(IPagSeguroGateway pagSeguroGateway)
    {
        _pagSeguroGateway = pagSeguroGateway;
    }

    public async Task<Transacao> RealizarPagamentoAsync(Guid pedidoId, decimal valorPedido, Pagamento pagamento)
    {
        var resultado = await _pagSeguroGateway.EfetivarTransacao(pedidoId,valorPedido, pagamento);
        var transacao = new Transacao(pagamento.Id, valorPedido, resultado.Id);

        if (resultado.Status == "PAID")
        {
            transacao.TransacaoPaga();
            return transacao;
        }
        
        transacao.TransacaoRecusada();
        return transacao;
    }
}