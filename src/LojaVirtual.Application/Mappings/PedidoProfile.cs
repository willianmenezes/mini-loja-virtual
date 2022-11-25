using AutoMapper;
using LojaVirtual.Application.Handlers.PedidoHandler.BuscarCarrinho;
using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Mappings;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, PedidoResponse>();
        CreateMap<PedidoItem, PedidoItemResponse>();
    }
}