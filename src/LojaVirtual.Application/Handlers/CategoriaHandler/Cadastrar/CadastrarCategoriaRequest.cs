using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;

public record CadastrarCategoriaRequest : IRequest<BaseResponse>
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}