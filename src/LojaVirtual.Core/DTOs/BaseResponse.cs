using LojaVirtual.Core.NotificationError;

namespace LojaVirtual.Core.DTOs;

public record BaseResponse
{
    public bool Status { get; set; }
    public object? Resultado { get; set; }
    public IEnumerable<NotificacaoErro>? Erros { get; set; }
    
    public static BaseResponse Erro(IEnumerable<NotificacaoErro>? erros = null)
    {
        return new BaseResponse
        {
            Erros = erros,
            Status = false
        };
    }
    
    public static BaseResponse Sucesso(object? resultado = null)
    {
        return new BaseResponse
        {
            Resultado = resultado,
            Status = true
        };
    }
}