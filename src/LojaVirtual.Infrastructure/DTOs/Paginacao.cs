using LojaVirtual.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.DTOs;

public class Paginacao<T> : List<T>, IPaginacao<T>
{
    public int TotalDePaginas { get; }
    public int Indice { get; }
    public int TotalDeItens { get; }

    public Paginacao()
    {
    }

    public Paginacao(List<T> itens, int totalDeItens, int indice, int tamanhoPagina)
    {
        Indice = indice;
        TotalDeItens = totalDeItens;
        TotalDePaginas = (int)Math.Ceiling(totalDeItens / (double)tamanhoPagina);
        AddRange(itens);
    }

    public async Task<IPaginacao<T>> CriarAsync(IQueryable<T> consulta, int indice, int tamanhoPagina)
    {
        var quantidadeDeItens = consulta.Count();
        var itens = await consulta.Skip((indice - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
        return new Paginacao<T>(itens, quantidadeDeItens, indice, tamanhoPagina);
    }
}