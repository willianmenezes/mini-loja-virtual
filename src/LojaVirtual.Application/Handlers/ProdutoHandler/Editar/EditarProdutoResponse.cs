namespace LojaVirtual.Application.Handlers.ProdutoHandler.Editar;

public record EditarProdutoResponse(Guid Id, Guid CategoriaId, string Nome, string Descricao, decimal Valor, bool Ativo, int QuantidadeEstoque);