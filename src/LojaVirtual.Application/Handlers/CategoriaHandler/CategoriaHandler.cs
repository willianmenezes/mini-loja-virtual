﻿using AutoMapper;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler;

public class CategoriaHandler :
    BaseHandler,
    IRequestHandler<CadastrarCategoriaRequest, BaseResponse>,
    IRequestHandler<ListarCategoriaRequest, BaseResponse>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IPaginacao<Categoria> _paginacaoCategorias;

    public CategoriaHandler(IPaginacao<Categoria> paginacaoCategorias, ICategoriaRepository categoriaRepository,
        IMediator mediator, IMapper mapper) : base(
        mediator, mapper)
    {
        _categoriaRepository = categoriaRepository;
        _paginacaoCategorias = paginacaoCategorias;
    }

    public async Task<BaseResponse> Handle(CadastrarCategoriaRequest request, CancellationToken cancellationToken)
    {
        if (Validar(request, new CadastrarCategoriaRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var categoria = Mapper.Map<Categoria>(request);
        await _categoriaRepository.AdicionarAsync(categoria);
        await _categoriaRepository.UnityOfWork.SalvarAlteracoes();

        return BaseResponse.Sucesso();
    }

    public async Task<BaseResponse> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
    {
        if (Validar(request, new ListarCategoriaRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var categorias =
            await _paginacaoCategorias.CriarAsync(_categoriaRepository.BuscarTodos(), request.Indice, request.TamanhoPagina);

        var categoriasResponse = Mapper.Map<IEnumerable<ListarCategoriaResponse>>(categorias);

        return BaseResponse.Sucesso(new PaginacaoResponse(
            categoriasResponse,
            categorias.TotalDePaginas,
            categorias.TotalDeItens,
            categorias.Indice,
            request.TamanhoPagina
        ));
    }
}