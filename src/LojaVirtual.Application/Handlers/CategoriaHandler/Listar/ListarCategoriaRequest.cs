using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Listar;

public record ListarCategoriaRequest : IRequest<BaseResponse>
{
    public int Indice { get; set; }
    public int TamanhoPagina { get; set; }
}