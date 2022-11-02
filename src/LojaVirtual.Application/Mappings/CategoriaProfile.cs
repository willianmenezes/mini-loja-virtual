using AutoMapper;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Mappings;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CadastrarCategoriaRequest, Categoria>()
            .ConstructUsing(request => new Categoria(request.Nome, request.Descricao));

        CreateMap<Categoria, ListarCategoriaResponse>();
    }
}