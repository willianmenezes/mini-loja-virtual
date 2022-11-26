using System.Text.Json.Serialization;
using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.PedidoHandler.FinalizarPedido;

public class FinalizarPedidoRequest : IRequest<BaseResponse>
{
    [JsonIgnore] public Guid UsuarioId { get; set; }
    public string NumeroCartao { get; set; }
    public string NomeCartao { get; set; }
    public string ExpiracaoCartao { get; set; }
    public string CodigoVerificadoCartao { get; set; }
}