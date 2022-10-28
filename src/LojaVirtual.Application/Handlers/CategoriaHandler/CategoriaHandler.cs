using AutoMapper;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler;

public class CategoriaHandler : BaseHandler, IRequestHandler<CadastrarCategoriaRequest, BaseResponse>
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaHandler(ICategoriaRepository categoriaRepository, IMediator mediator, IMapper mapper) : base(
        mediator, mapper)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<BaseResponse> Handle(CadastrarCategoriaRequest request, CancellationToken cancellationToken)
    {
        if (Validar(request, new CadastrarCategoriaRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var categoria = Mapper.Map<Categoria>(request);
        await _categoriaRepository.Adicionar(categoria);
        await _categoriaRepository.UnityOfWork.SalvarAlteracoes();

        return BaseResponse.Sucesso();
    }
}