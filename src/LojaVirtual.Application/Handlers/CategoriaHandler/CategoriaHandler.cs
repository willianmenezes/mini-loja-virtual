﻿using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Editar;
using LojaVirtual.Application.Handlers.CategoriaHandler.Listar;
using LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.Handlers;
using LojaVirtual.Core.NotificationError;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler;

public class CategoriaHandler :
    BaseHandler,
    IRequestHandler<CadastrarCategoriaRequest, BaseResponse>,
    IRequestHandler<ListarCategoriaRequest, BaseResponse>,
    IRequestHandler<ListarCategoriaPorIdRequest, BaseResponse>,
    IRequestHandler<EditarCategoriaRequest, BaseResponse>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IPaginacao<Categoria> _paginacaoCategorias;

    public CategoriaHandler(
        IPaginacao<Categoria> paginacaoCategorias,
        ICategoriaRepository categoriaRepository,
        IMediator mediator,
        IMapper mapper) : base(mediator, mapper)
    {
        _categoriaRepository = categoriaRepository;
        _paginacaoCategorias = paginacaoCategorias;
    }

    public async Task<BaseResponse> Handle(CadastrarCategoriaRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new CadastrarCategoriaRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var categoria = Mapper.Map<Categoria>(request);
        await _categoriaRepository.AdicionarAsync(categoria);
        await _categoriaRepository.UnityOfWork.SalvarAlteracoesAsync();

        return BaseResponse.Sucesso();
    }

    public async Task<BaseResponse> Handle(ListarCategoriaRequest categoriaRequest, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(categoriaRequest, new ListarCategoriaRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var categorias =
            await _paginacaoCategorias.CriarAsync(_categoriaRepository.BuscarTodos(), categoriaRequest.Indice,
                categoriaRequest.TamanhoPagina);

        var categoriasResponse = Mapper.Map<IEnumerable<ListarCategoriaResponse>>(categorias);

        return BaseResponse.Sucesso(new PaginacaoResponse(
            categoriasResponse,
            categorias.TotalDePaginas,
            categorias.TotalDeItens,
            categorias.Indice,
            categoriaRequest.TamanhoPagina
        ));
    }

    public async Task<BaseResponse> Handle(ListarCategoriaPorIdRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new ListarCategoriaPorIdRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        if (await _categoriaRepository.BuscarPorIdAsync(request.Id) is var categoria && categoria is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(ListarCategoriaPorIdRequest)}",
                "Categoria não encontrada."));
            return BaseResponse.Erro();
        }

        var categoriasResponse = Mapper.Map<ListarCategoriaPorIdResponse>(categoria);
        return BaseResponse.Sucesso(categoriasResponse);
    }

    public async Task<BaseResponse> Handle(EditarCategoriaRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new EditarCategoriaRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        if (await _categoriaRepository.BuscarPorIdAsync(request.Id) is var categoria && categoria is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(EditarCategoriaRequest)}",
                "Categoria não encontrada."));
            return BaseResponse.Erro();
        }

        categoria.AlterarNome(request.Nome);
        categoria.AlterarDescricao(request.Descricao);

        if (request.Ativo)
            categoria.Ativar();
        else
            categoria.Desativar();

        await _categoriaRepository.UnityOfWork.SalvarAlteracoesAsync();
        return BaseResponse.Sucesso(Mapper.Map<EditarCategoriaResponse>(categoria));
    }
}