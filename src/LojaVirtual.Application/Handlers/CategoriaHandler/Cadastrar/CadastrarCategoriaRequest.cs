using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;

public record CadastrarCategoriaRequest(string Nome, string Descricao) : IRequest<BaseResponse>;
