using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Maquinas
{
    public class MaquinaValidator : AbstractValidator<MaquinaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public MaquinaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirMaquinaValidator : AbstractValidator<MaquinaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirMaquinaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((modelomaq, validateContext) =>
            {

                var princ = context.maquinasparametro.FirstOrDefault(c => c.IdMaquina == modelomaq.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem parâmetros para essa máquina");
                }
                var princ1 = context.maquinasplanejadas.FirstOrDefault(c => c.IdMaquina == modelomaq.Id);
                if (princ1 != null)
                {
                    validateContext.AddFailure("Maquina com planejamento");
                }

            });
        }

    }
}
