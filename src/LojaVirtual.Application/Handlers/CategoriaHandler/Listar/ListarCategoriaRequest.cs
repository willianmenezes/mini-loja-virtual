using LojaVirtual.Application.DTOs;
using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Listar;

public record ListarCategoriaRequest : PaginacaoRequest
{
}