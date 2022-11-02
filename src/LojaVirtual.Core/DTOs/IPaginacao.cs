namespace LojaVirtual.Core.DTOs;

public interface IPaginacao<T>
{
    int TotalDePaginas { get; }
    int Indice { get; }
    int TotalDeItens { get; }

    Task<IPaginacao<T>> CriarAsync(IQueryable<T> consulta, int indice, int tamanhoPagina);
}