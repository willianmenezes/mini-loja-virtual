using System.Net;
using System.Text.Json;
using LojaVirtual.Core.Domain;
using LojaVirtual.Core.DTOs;
using LojaVirtual.Core.NotificationError;

namespace LojaVirtual.WEB.Configurations;

public class MainErrorHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public MainErrorHandler(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger("Main Error Handler");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            Console.WriteLine("indooooooooooo");
            await _next.Invoke(httpContext);
            Console.WriteLine("Voltando");
        }
        catch (Exception exception)
        {
            await HandlerErrorAsync(httpContext, exception);
        }
    }

    private async Task HandlerErrorAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        BaseResponse baseResponse;
        NotificacaoErro notificacaoErro;
        HttpStatusCode httpStatusCode;

        switch (exception)
        {
            case DomainException:
                notificacaoErro = new NotificacaoErro("Validação de domínio", exception.Message);
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
            case ArgumentNullException or ArgumentException:
                notificacaoErro = new NotificacaoErro("Argumentos inválidos", exception.Message);
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
            default:
                notificacaoErro = new NotificacaoErro("Erro de servidor", "Erro genérico, consulte os administradores.");
                httpStatusCode = HttpStatusCode.InternalServerError;
                break;
        }

        httpContext.Response.StatusCode = (int)httpStatusCode;
        baseResponse = BaseResponse.Erro(new List<NotificacaoErro> { notificacaoErro });
        _logger.LogError($"Erro: {exception.Message}");
        _logger.LogError($"StackTrace: {exception.StackTrace}");

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(baseResponse));
    }
}