namespace LojaVirtual.Application.Handlers.ProdutoHandler.Listar;

public record ListarProdutoResponse(Guid Id, string Nome, string Descricao, bool Ativo, decimal Valor,
    int QuantidadeEstoque);