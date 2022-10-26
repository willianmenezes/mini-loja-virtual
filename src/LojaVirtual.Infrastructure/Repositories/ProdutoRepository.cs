using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _dbContext;
    public IUnityOfWork UnityOfWork => _dbContext;

    public ProdutoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Adicionar(Produto produto)
    {
        await _dbContext.Produtos.AddAsync(produto);
    }

    public IQueryable<Produto> BuscarTodos()
    {
        return _dbContext.Produtos.Where(p => p.Ativo);
    }
}