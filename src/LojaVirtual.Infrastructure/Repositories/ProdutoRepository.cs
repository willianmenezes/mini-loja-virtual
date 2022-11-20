using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;

namespace LojaVirtual.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _context;
    public IUnityOfWork UnityOfWork => _context;

    public ProdutoRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AdicionarAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
    }

    public Task<Produto?> BuscarPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Produto> BuscarTodos()
    {
        return _context.Produtos.Where(p => p.Ativo);
    }
}