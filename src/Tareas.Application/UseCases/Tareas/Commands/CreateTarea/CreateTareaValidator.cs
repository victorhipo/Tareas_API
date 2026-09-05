using FluentValidation;
using Microsoft.VisualBasic;

namespace Tareas.Application.UseCases.Tareas.Commands.CreateTarea.CreateTareaValidator;

public class CreateTareaValidator : AbstractValidator<CreateTareaCommand>
{
    public CreateTareaValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("El título es obligatorio.")
                            .MaximumLength(200).WithMessage("El título no puede exceder los 200 caracteres.");
        
        RuleFor(x => x.Description).MaximumLength(2000).WithMessage("La descripción no puede exceder los 2000 caracteres");

        RuleFor(x => x.DueDate).GreaterThan(DateTime.UtcNow).When(x => x.DueDate.HasValue).WithMessage("La fecha de vencimiento debe ser futura");

    }
}