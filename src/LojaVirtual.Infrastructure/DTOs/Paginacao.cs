using LojaVirtual.Domain.Interfaces.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Application.DTOs;

public class Paginacao<T> : List<T>, IPaginacao<T>
{
    public int TotalDePaginas { get; private set; }
    public int Indice { get; private set; }
    public int TotalDeItens { get; private set; }

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

    public async Task<IPaginacao<T>> Criar(IQueryable<T> consulta, int indice, int tamanhoPagina)
    {
        var quantidadeDeItens = consulta.Count();
        var itens = await consulta.Skip((indice - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
        return new Paginacao<T>(itens, quantidadeDeItens, indice, tamanhoPagina);
    }
}