using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LojaVirtual.Infrastructure.Pagamentos.Gateways;

public class PagSeguroGatewayResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("reference_id")]
    public string IdReferencia { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("created_at")]
    public string DataCriacao { get; set; }
    
    [JsonPropertyName("paid_at")]
    public string DataPagamento { get; set; }    
    
    [JsonPropertyName("description")]
    public string Descricao { get; set; }    
    
    [JsonPropertyName("metadata")]
    public object Metadados { get; set; }
    
    [JsonPropertyName("amount")]
    public AmountResponse Cobranca { get; set; }
    
    [JsonPropertyName("payment_response")]
    public PaymentResponse Pagamento { get; set; }
    
    [JsonPropertyName("payment_method")]
    public PaymentMethodResponse MetodoPagamento { get; set; }
}

public class PaymentMethodResponse
{
    [JsonPropertyName("type")]
    public string Tipo { get; set; } 
    
    [JsonPropertyName("installments")]
    public int QuantidadeParcelas { get; set; } 
    
    [JsonPropertyName("capture")]
    public bool CobrancaEmUmPasso { get; set; }     
    
    [JsonPropertyName("card")]
    public CardResponse Cartao { get; set; } 
}

public class CardResponse
{
    [JsonPropertyName("brand")]
    public string Bandeira { get; set; } 
    
    [JsonPropertyName("first_digits")]
    public string DigitosIniciais { get; set; } 
    
    [JsonPropertyName("last_digits")]
    public string UltimosDigitos { get; set; } 
    
    [JsonPropertyName("exp_month")]
    public string MesVencimento { get; set; } 
    
    [JsonPropertyName("exp_year")]
    public string AnoVencimento { get; set; }    
    
    [JsonPropertyName("holder")]
    public HolderResponse PortadorCartao { get; set; } 
}

public class HolderResponse
{
    [JsonPropertyName("name")]
    public string Nome { get; set; }
}

public class PaymentResponse
{
    [JsonPropertyName("code")]
    public string Codigo { get; set; } 
    
    [JsonPropertyName("message")]
    public string Mensagem { get; set; } 
    
    [JsonPropertyName("reference")]
    public string Referencia { get; set; } 
}

public class AmountResponse
{
    [JsonPropertyName("amount")]
    public int Valor { get; set; }
    
    [JsonPropertyName("currency")]
    public string Moeda { get; set; }
    
    [JsonPropertyName("summary")]
    public AmountSumary DadosCobranca { get; set; }
}

public class AmountSumary
{
    [JsonPropertyName("total")]
    public int ValorTotal { get; set; }
    
    [JsonPropertyName("paid")]
    public int ValorPago { get; set; }
    
    [JsonPropertyName("refunded")]
    public int ValorDevolvido { get; set; }
}