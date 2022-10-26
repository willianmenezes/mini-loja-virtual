using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface ICategoriaRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task Adicionar(Categoria categoria);
    IQueryable<Categoria> BuscarTodos();
}