using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Core.Integration;

public class CancelarPedidoRequest : IRequest<BaseResponse>
{
    public Guid PedidoId { get; set; }
}