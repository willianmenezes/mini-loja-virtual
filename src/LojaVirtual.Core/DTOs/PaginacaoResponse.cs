namespace LojaVirtual.Core.DTOs;

public record PaginacaoResponse(object Itens, int TotalDePaginas, int TotalDeItens, int Indice, int TamanhoPagina);