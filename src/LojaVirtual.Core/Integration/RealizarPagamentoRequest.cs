using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Core.Integration;

public class RealizarPagamentoRequest : IRequest<BaseResponse>
{
    public Guid PedidoId { get; set; }
    public Guid UsuarioId { get; set; }
    public decimal Total { get; set; }
    public string NumeroCartao { get; set; }
    public string NomeCartao { get; set; }
    public string ExpiracaoCartao { get; set; }
    public string CodigoVerificadoCartao { get; set; }
}