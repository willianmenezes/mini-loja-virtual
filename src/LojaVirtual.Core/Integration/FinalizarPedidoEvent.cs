using MediatR;

namespace LojaVirtual.Core.Integration;

public class FinalizarPedidoEvent : INotification
{
    public Guid PedidoId { get; set; }
    public Guid UsuarioId { get; set; }
    public decimal Total { get; set; }
    public string NumeroCartao { get; set; }
    public string NomeCartao { get; set; }
    public string ExpiracaoCartao { get; set; }
    public string CodigoVerificadoCartao { get; set; }
    public IEnumerable<Item> Itens { get; set; }
}

public class Item
{
    public Guid Id { get; set; }
    public int Quantidade { get; set; }
}