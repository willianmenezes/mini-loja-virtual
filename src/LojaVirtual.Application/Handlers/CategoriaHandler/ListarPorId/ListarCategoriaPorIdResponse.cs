namespace LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;

public record  ListarCategoriaPorIdResponse(Guid Id, string Nome, string Descricao, bool Ativo);