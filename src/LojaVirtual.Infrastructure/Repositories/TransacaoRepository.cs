using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Interfaces.Repositories;
using LojaVirtual.Infrastructure.Context;

namespace LojaVirtual.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly ApplicationDbContext _context;

    public TransacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnityOfWork UnityOfWork => _context;
    
    public async Task AdicionarAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
    }
}