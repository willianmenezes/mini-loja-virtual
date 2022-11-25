using System.Text.Json.Serialization;
using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;

public class AdicionarItemPedidoRequest : IRequest<BaseResponse>
{
    [JsonIgnore] public Guid UsuarioId { get; set; }
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
}