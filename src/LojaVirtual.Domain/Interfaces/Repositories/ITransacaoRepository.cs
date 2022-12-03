using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Domain.Interfaces.Repositories;

public interface ITransacaoRepository
{
    IUnityOfWork UnityOfWork { get; }
    Task AdicionarAsync(Transacao transacao);
}