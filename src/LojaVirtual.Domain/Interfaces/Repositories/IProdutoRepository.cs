using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface IProdutoRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task AdicionarAsync(Produto produto);
    Task<Produto?> BuscarPorIdAsync(Guid id);
    IQueryable<Produto> BuscarTodos();
}