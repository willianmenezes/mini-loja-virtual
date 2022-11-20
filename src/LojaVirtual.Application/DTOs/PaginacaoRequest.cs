using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.DTOs;

public record PaginacaoRequest : IRequest<BaseResponse>
{
    public int Indice { get; set; }
    public int TamanhoPagina { get; set; }
}