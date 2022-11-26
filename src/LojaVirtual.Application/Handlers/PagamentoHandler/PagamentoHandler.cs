using AutoMapper;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.Integration;
using MediatR;

namespace LojaVirtual.Application.Handlers.PagamentoHandler;

public class PagamentoHandler : 
    BaseHandler,
    IRequestHandler<RealizarPagamentoRequest, BaseResponse>
{
    public PagamentoHandler(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    public async Task<BaseResponse> Handle(RealizarPagamentoRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}