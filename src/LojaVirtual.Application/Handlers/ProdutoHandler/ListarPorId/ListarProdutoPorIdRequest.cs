using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;

public record ListarProdutoPorIdRequest(Guid Id) : IRequest<BaseResponse>;