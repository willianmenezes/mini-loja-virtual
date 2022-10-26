using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface IProdutoRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task Adicionar(Produto produto);
    Task<IQueryable<Produto>> BuscarTodos();
}