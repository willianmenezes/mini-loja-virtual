﻿using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Produto?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Produto> BuscarTodos()
    {
        return _context.Produtos
            .AsNoTracking().Where(p => p.Ativo);
    }
}