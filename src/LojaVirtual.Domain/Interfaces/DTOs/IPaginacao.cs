namespace LojaVirtual.Domain.Interfaces.DTOs;

public interface IPaginacao<T>
{
    Task<IPaginacao<T>> Criar(IQueryable<T> consulta, int indice, int tamanhoPagina);
}