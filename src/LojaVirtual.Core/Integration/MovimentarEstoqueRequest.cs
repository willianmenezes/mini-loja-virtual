using LojaVirtual.Core.DTOs;
using MediatR;

namespace LojaVirtual.Core.Integration;

public class MovimentarEstoqueRequest : IRequest<bool>
{
    public IEnumerable<Item> Itens { get; set; }
}