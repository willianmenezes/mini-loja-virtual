using MediatR;

namespace LojaVirtual.Core.Integration;

public class ApplicationEventsHandler : INotificationHandler<FinalizarPedidoEvent>
{
    private readonly IMediator _mediator;

    public ApplicationEventsHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(FinalizarPedidoEvent notification, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(new MovimentarEstoqueRequest() { Itens = notification.Itens }, cancellationToken);

        if (resultado)
        {
            _ = await _mediator.Send(new RealizarPagamentoRequest
            {
                UsuarioId = notification.UsuarioId,
                PedidoId = notification.PedidoId,
                Total = notification.Total,
                ExpiracaoCartao = notification.ExpiracaoCartao,
                NomeCartao = notification.NomeCartao,
                NumeroCartao = notification.NumeroCartao,
                CodigoVerificadoCartao = notification.CodigoVerificadoCartao
            }, cancellationToken);
        }
        else
        {
            _ = await _mediator.Send(new CancelarPedidoRequest { PedidoId = notification.PedidoId }, cancellationToken);
        }
    }
}