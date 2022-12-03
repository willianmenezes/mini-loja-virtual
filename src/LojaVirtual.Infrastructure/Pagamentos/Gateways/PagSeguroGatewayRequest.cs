using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LojaVirtual.Infrastructure.Pagamentos.Gateways;

public class PagSeguroGatewayRequest
{
    [JsonPropertyName("reference_id")]
    public string IdReferencia { get; set; }
    
    [JsonPropertyName("description")]
    public string Descricao { get; set; }
    
    [JsonPropertyName("amount")]
    public Amount Cobranca { get; set; }
    
    [JsonPropertyName("payment_method")]
    public PaymentMethod MetodoPagamento { get; set; }
    
    [JsonPropertyName("notification_urls")]
    public IEnumerable<string> UrlsNotificacao { get; set; }
}

public class Amount
{
    [JsonPropertyName("value")]
    public int Valor { get; set; }
    
    [JsonPropertyName("currency")]
    public string Moeda { get; set; }
}

public class PaymentMethod
{
    [JsonPropertyName("type")]
    public string Tipo { get; set; }
    
    [JsonPropertyName("installments")]
    public int QuantidadeParcelas { get; set; }
    
    [JsonPropertyName("capture")]
    public bool CobrancaEmUmPasso { get; set; }
    
    [JsonPropertyName("soft_descriptor")]
    public string NomeFaturaCartao { get; set; }
    
    [JsonPropertyName("card")]
    public Card Cartao { get; set; }
}

public class Card
{
    [JsonPropertyName("number")]
    public string Numero { get; set; }
    
    [JsonPropertyName("exp_month")]
    public string VencimentoMes { get; set; }
    
    [JsonPropertyName("exp_year")]
    public string VencimentoAno { get; set; }
    
    [JsonPropertyName("security_code")]
    public string CodigoSeguranca { get; set; }
    
    [JsonPropertyName("store")]
    public bool ArmazenarCartao { get; set; }
    
    [JsonPropertyName("holder")]
    public Holder PortadorCartao { get; set; }
}

public class Holder
{
    [JsonPropertyName("name")]
    public string Nome { get; set; }
}