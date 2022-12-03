using LojaVirtual.Domain.Entities;
using LojaVirtual.Infrastructure.HttpClients.Pagamento;

namespace LojaVirtual.Infrastructure.Pagamentos.Gateways;

public class PagSeguroGateway : IPagSeguroGateway
{
    private readonly IHttpClientPagamentoService _httpClientPagamentoService;

    public PagSeguroGateway(IHttpClientPagamentoService httpClientPagamentoService)
    {
        _httpClientPagamentoService = httpClientPagamentoService;
    }

    public async Task<PagSeguroGatewayResponse> EfetivarTransacao(Guid pedidoId, decimal valorPedido,
        Pagamento pagamento)
    {
        var request = new PagSeguroGatewayRequest
        {
            Descricao = $"Pagamento feito de forma online - id pedido: {pedidoId}",
            Cobranca = new Amount
            {
                Moeda = "BRL",
                Valor = (int)(valorPedido * 100),
            },
            MetodoPagamento = new PaymentMethod
            {
                Tipo = "CREDIT_CARD",
                QuantidadeParcelas = 1,
                CobrancaEmUmPasso = true,
                NomeFaturaCartao = "Lojar Virtual",
                Cartao = new Card
                {
                    Numero = pagamento.NumeroCartaoCredito,
                    CodigoSeguranca = pagamento.DigitoVerificadorCartaoCredito,
                    PortadorCartao = new Holder
                    {
                        Nome = pagamento.NomeCartaoCredito
                    },
                    VencimentoMes = pagamento.ExpiracaoCartaoCredito.Split("/")[0],
                    VencimentoAno = pagamento.ExpiracaoCartaoCredito.Split("/")[1],
                }
            },
            UrlsNotificacao = new List<string>()
            {
                "https://yourserver.com/nas_ecommerce/277be731-3b7c-4dac-8c4e-4c3f4a1fdc46/"
            }
        };

        return await _httpClientPagamentoService.PostAsync<PagSeguroGatewayResponse, PagSeguroGatewayRequest>(request);
    }
}