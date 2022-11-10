namespace LojaVirtual.Core.Data;

public interface IUnityOfWork
{
    Task SalvarAlteracoesAsync();
}