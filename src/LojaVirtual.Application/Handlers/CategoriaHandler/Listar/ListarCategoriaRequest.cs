using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Listar;

public record ListarCategoriaRequest(int Indice, int TamanhoPagina) : IRequest<BaseResponse>;
