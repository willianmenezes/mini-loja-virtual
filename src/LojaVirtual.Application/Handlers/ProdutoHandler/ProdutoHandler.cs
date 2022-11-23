using AutoMapper;
using LojaVirtual.Application.Handlers.ProdutoHandler.Cadastrar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Editar;
using LojaVirtual.Application.Handlers.ProdutoHandler.Listar;
using LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using MediatR;

namespace LojaVirtual.Application.Handlers.ProdutoHandler;

public class ProdutoHandler :
    BaseHandler,
    IRequestHandler<CadastrarProdutoRequest, BaseResponse>,
    IRequestHandler<ListarProdutoRequest, BaseResponse>,
    IRequestHandler<ListarProdutoPorIdRequest, BaseResponse>,
    IRequestHandler<EditarProdutoRequest, BaseResponse>
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

    public async Task<BaseResponse> Handle(ListarProdutoPorIdRequest request, CancellationToken cancellationToken)
    {
        
        if (await ValidarAsync(request, new ListarProdutoPorIdRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        if (await _produtoRepository.BuscarPorIdAsync(request.Id) is var produto && produto is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(ListarProdutoPorIdRequest)}",
                "Produto não encontrado."));
            return BaseResponse.Erro();
        }

        var produtoResponse = Mapper.Map<ListarProdutoPorIdResponse>(produto);
        return BaseResponse.Sucesso(produtoResponse);
    }

    public async Task<BaseResponse> Handle(EditarProdutoRequest request, CancellationToken cancellationToken)
    {
        if (await ValidarAsync(request, new EditarProdutoRequestValidator()) is var resultado && !resultado)
            return BaseResponse.Erro();

        if (await _produtoRepository.BuscarPorIdAsync(request.Id) is var produto && produto is null)
        {
            await Mediator.Publish(new NotificacaoErro($"{nameof(EditarProdutoRequest)}",
                "Produto não encontrada."));
            return BaseResponse.Erro();
        }

        produto.AlterarNome(request.Nome);
        produto.AlterarDescricao(request.Descricao);
        produto.AlterarValor(request.Valor);

        if (produto.PossuiCategoria(request.CategoriaId) == false)
            produto.AlterarCategoria(request.CategoriaId);

        if (request.Ativo)
            produto.Ativar();
        else
            produto.Desativar();

        await _produtoRepository.UnityOfWork.SalvarAlteracoesAsync();
        return BaseResponse.Sucesso(Mapper.Map<EditarProdutoResponse>(produto));
    }
}