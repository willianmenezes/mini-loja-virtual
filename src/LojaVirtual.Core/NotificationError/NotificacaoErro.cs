using MediatR;

namespace LojaVirtual.Core.NotificationError;

public class NotificacaoErro : INotification
{
    public string NomeProcesso { get; set; }
    public string Mensagem { get; set; }

    public NotificacaoErro(string nomeProcesso, string mensagem)
    {
        NomeProcesso = nomeProcesso;
        Mensagem = mensagem;
    }
}