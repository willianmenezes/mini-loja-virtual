using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface IPagamentoRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task AdicionarAsync(Pagamento pagamento);
}