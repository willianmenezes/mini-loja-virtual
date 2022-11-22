namespace LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;

public record ListarProdutoPorIdResponse(Guid Id, string Nome, string Descricao, decimal Valor, int QuantidadeEstoque,
    ListarProdutoPorIdCategoriaResponse Categoria);

public record ListarProdutoPorIdCategoriaResponse(Guid Id, string Nome, string Descricao, bool Ativo);