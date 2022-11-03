using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;

public record ListarCategoriaPorIdRequest(Guid Id) : IRequest<BaseResponse>;