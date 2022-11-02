using FluentValidation;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Listar;

public class ListarCategoriaRequestValidator : AbstractValidator<ListarCategoriaRequest>
{
    public ListarCategoriaRequestValidator()
    {
        RuleFor(p => p.Indice)
            .GreaterThan(0).WithMessage("O índice da página deve ser maior que {PropertyValue}.");

        RuleFor(p => p.TamanhoPagina)
            .GreaterThan(0).WithMessage("O tamanho da página deve ser maior que {PropertyValue}")
            .LessThan(100).WithMessage("O tamanho da página deve ser menor que {PropertyValue}");
    }
}