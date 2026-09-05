using FluentValidation;

namespace Tareas.Application.UseCases.Tareas.Commands.UpdateTarea;

public class UpdateTareaValidator : AbstractValidator<UpdateTareaCommand>
{
    public UpdateTareaValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El id es obligatorio.");

        RuleFor(x => x.Title).NotEmpty().WithMessage("El título es obligatorio.")
                            .MaximumLength(200).WithMessage("El título no puede exceder los 200 caracteres.");
        
        RuleFor(x => x.Description).MaximumLength(2000).WithMessage("La descripción no puede exceder los 2000 caracteres.");

        RuleFor(x => x.Status).IsInEnum().WithMessage("El estado no es válido.");
    }
}