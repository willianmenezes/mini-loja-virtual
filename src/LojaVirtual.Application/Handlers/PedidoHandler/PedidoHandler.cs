using AutoMapper;
using MediatR;

namespace LojaVirtual.Application.Handlers.PedidoHandler;

public class PedidoHandler : BaseHandler
{
    public PedidoHandler(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }
}