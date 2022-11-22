namespace LojaVirtual.Application.Handlers.ProdutoHandler.Editar;

public record EditarProdutoRequest(Guid Id, string Nome, string Descricao, int QuantidadeEstoque, bool Ativo,
    decimal Valor, Guid CategoriaId);