using FluentValidation;

namespace LojaVirtual.Application.Handlers.PedidoHandler.FinalizarPedido;

public class FinalizarPedidoRequestValidator : AbstractValidator<FinalizarPedidoRequest>
{
    public FinalizarPedidoRequestValidator()
    {
        RuleFor(x => x.UsuarioId)
            .NotEqual(Guid.Empty).WithMessage("Usuário inválido.");

        RuleFor(x => x.ExpiracaoCartao)
            .NotEmpty().WithMessage("Data de expiração obrigatória");
        
        RuleFor(x => x.NomeCartao)
            .NotEmpty().WithMessage("Nome do cartão obrigatório");
        
        RuleFor(x => x.NumeroCartao)
            .NotEmpty().WithMessage("Número do cartão obrigatório");
        
        RuleFor(x => x.CodigoVerificadoCartao)
            .NotEmpty().WithMessage("CVV obrigatório");
    }
}