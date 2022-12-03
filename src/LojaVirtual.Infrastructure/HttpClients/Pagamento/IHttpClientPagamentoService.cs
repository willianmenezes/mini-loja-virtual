namespace LojaVirtual.Infrastructure.HttpClients.Pagamento;

public interface IHttpClientPagamentoService
{
    Task<TResponse> PostAsync<TResponse, TRequest>(TRequest request)
        where TResponse : class where TRequest : class;
}