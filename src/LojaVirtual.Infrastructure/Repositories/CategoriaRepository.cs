using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _dbContext;
    public IUnityOfWork UnityOfWork => _dbContext;

    public CategoriaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AdicionarAsync(Categoria categoria)
    {
        await _dbContext.Categorias.AddAsync(categoria);
    }

    public async Task<Categoria?> BuscarPorIdAsync(Guid id)
    {
        return await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Categoria> BuscarTodos()
    {
        return _dbContext.Categorias.Where(p => p.Ativo);
    }
}