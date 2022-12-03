using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;

namespace LojaVirtual.Infrastructure.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly ApplicationDbContext _context;

    public PagamentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnityOfWork UnityOfWork => _context;
    
    public async Task AdicionarAsync(Pagamento pagamento)
    {
        await _context.Pagamentos.AddAsync(pagamento);
    }
}