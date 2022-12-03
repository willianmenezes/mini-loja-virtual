using LojaVirtual.Core.Domain;

namespace LojaVirtual.Domain.Entities;

public class Pagamento : Entity
{
    public decimal Valor { get; private set; }
    public Guid PedidoId { get; private set; }
    public string NomeCartaoCredito { get; private set; }
    public string NumeroCartaoCredito { get; private set; }
    public string ExpiracaoCartaoCredito { get; private set; }
    public string DigitoVerificadorCartaoCredito { get; private set; }
    
    // EF relation
    public Transacao Transacao { get; set; }
    public Pedido Pedido { get; set; }
    
    public Pagamento(Guid pedidoId, decimal valor, string nomeCartaoCredito, string numeroCartaoCredito, string expiracaoCartaoCredito, string digitoVerificadorCartaoCredito)
    {
        PedidoId = pedidoId;
        Valor = valor;
        NomeCartaoCredito = nomeCartaoCredito;
        NumeroCartaoCredito = numeroCartaoCredito;
        ExpiracaoCartaoCredito = expiracaoCartaoCredito;
        DigitoVerificadorCartaoCredito = digitoVerificadorCartaoCredito;
    }
    
    public override void Validar()
    {
        throw new NotImplementedException();
    }
}