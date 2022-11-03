using FluentValidation;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.ListarPorId;

public class ListarCategoriaPorIdRequestValidator : AbstractValidator<ListarCategoriaPorIdRequest>
{
    public ListarCategoriaPorIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O id é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("Id inválido.");
    }
}