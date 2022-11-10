using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Editar;

public record EditarCategoriaRequest(Guid Id, string Nome, string Descricao, bool Ativo) : IRequest<BaseResponse>;