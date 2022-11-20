using AutoMapper;
using LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Listar;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.CategoriaHandler;

public class ProdutoHandler :
    BaseHandler,
    IRequestHandler<CadastrarProdutoRequest, BaseResponse>,
    IRequestHandler<ListarProdutoRequest, BaseResponse>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IPaginacao<Produto> _paginacaoProdutos;

    public ProdutoHandler(
        IPaginacao<Produto> paginacaoProdutos,
        IProdutoRepository produtoRepository,
        IMediator mediator,
        IMapper mapper) : base(mediator, mapper)
    {
        _produtoRepository = produtoRepository;
        _paginacaoProdutos = paginacaoProdutos;
    }

    public async Task<BaseResponse> Handle(CadastrarProdutoRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new CadastrarProdutoRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var produto = Mapper.Map<Produto>(request);
        await _produtoRepository.AdicionarAsync(produto);
        await _produtoRepository.UnityOfWork.SalvarAlteracoesAsync();

        return BaseResponse.Sucesso();
    }

    public async Task<BaseResponse> Handle(ListarProdutoRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new ListarProdutoRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        var produtos =
            await _paginacaoProdutos.CriarAsync(_produtoRepository.BuscarTodos(), request.Indice,
                request.TamanhoPagina);

        var produtosResponse = Mapper.Map<IEnumerable<ListarProdutoResponse>>(produtos);

        return BaseResponse.Sucesso(new PaginacaoResponse(
            produtosResponse,
            produtos.TotalDePaginas,
            produtos.TotalDeItens,
            produtos.Indice,
            request.TamanhoPagina
        ));
    }
}