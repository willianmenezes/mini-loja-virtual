using MediatR;

namespace LojaVirtual.Core.NotificationError;

public class NotificacaoErroHandler : INotificationHandler<NotificacaoErro>
{
    private readonly ICollection<NotificacaoErro> _erros;

    public NotificacaoErroHandler(IEnumerable<NotificacaoErro> erros)
    {
        _erros = new List<NotificacaoErro>();
    }

    public async Task Handle(NotificacaoErro notification, CancellationToken cancellationToken)
    {
        _erros.Add(notification);
        await Task.CompletedTask;
    }

    public IEnumerable<NotificacaoErro> ObterErros() => _erros;
    public bool ExisteErros() => _erros.Any();
}