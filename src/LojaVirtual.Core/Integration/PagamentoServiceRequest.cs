namespace LojaVirtual.Core.Integration;

public class PagamentoServiceRequest
{
    public Guid PedidoId { get; set; }
    public Guid UsuarioId { get; set; }
    public decimal Total { get; set; }
    public string NomeCartao { get; set; }
    public string NumeroCartao { get; set; }
    public string ExpiracaoCartao { get; set; }
    public string CvvCartao { get; set; }
}