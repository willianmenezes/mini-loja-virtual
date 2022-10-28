using FluentValidation;

namespace LojaVirtual.Application.Handlers.CategoriaHandler.Cadastrar;

public class CadastrarCategoriaRequestValidator : AbstractValidator<CadastrarCategoriaRequest>
{
    public CadastrarCategoriaRequestValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatória.")
            .MinimumLength(3).WithMessage("O nome da categoria deve ter mais que {MinLength} caracteres.")
            .MaximumLength(200).WithMessage("O nome da categoria deve ter menos que {MaxLength} caracteres.");
        
        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("O nome da descricao e obrigatória.")
            .MinimumLength(3).WithMessage("O nome da descricao deve ter mais que {MinLength} caracteres.")
            .MaximumLength(200).WithMessage("O nome da descricao deve ter menos que {MaxLength} caracteres.");
    }
}