using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Maquinas;

using FluentValidation;

namespace FarmPlannerAPI.Validators.Maquinas
{
    public class MarcaMaquinaValidator : AbstractValidator<MarcaMaquinaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public MarcaMaquinaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("E necessario informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirMarcaMaquinaValidator : AbstractValidator<MarcaMaquinaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirMarcaMaquinaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((modelomaq, validateContext) =>
            {
                var princ = context.modelosmaquinas.FirstOrDefault(c => c.IdMarca == modelomaq.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem modelos vinculados a marca");
                }
            });
        }
    }
}