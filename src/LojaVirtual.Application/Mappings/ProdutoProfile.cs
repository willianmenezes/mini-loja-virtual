using AutoMapper;
using LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Listar;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Mappings;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<CadastrarProdutoRequest, Produto>()
            .ConstructUsing(x => new Produto(x.Nome, x.Descricao, x.Valor, x.QuantidadeEstoque, x.CategoriaId));

        CreateMap<Produto, ListarProdutoResponse>();
    }
}