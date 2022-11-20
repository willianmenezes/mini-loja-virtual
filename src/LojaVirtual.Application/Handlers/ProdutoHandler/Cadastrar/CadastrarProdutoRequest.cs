using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;

public record CadastrarProdutoRequest(
    string Nome,
    string Descricao,
    decimal Valor,
    int QuantidadeEstoque,
    Guid CategoriaId) : IRequest<BaseResponse>;