using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Enums;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    public IUnityOfWork UnityOfWork => _context;
    private readonly ApplicationDbContext _context;
    
    public PedidoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> BuscarPedidoIniciadoPorIdUsuario(Guid usuarioId)
    {
        var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.Status == StatusPedido.EmAndamento);
        if (pedido == null) return null;

        await _context.Entry(pedido)
            .Collection(i => i.PedidoItens).LoadAsync();

        return pedido;
    }

    public IQueryable<Pedido> ObterPedidosPorUsuario(Guid usuarioId)
    {
        return _context.Pedidos
            .AsNoTracking()
            .Where(p => p.UsuarioId == usuarioId && p.Status != StatusPedido.EmAndamento);
    }

    public void Adicionar(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
    }

    public async Task<Pedido?> BuscarPorId(Guid id)
    {
        return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
    }
}