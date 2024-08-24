using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Tecnologia;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Tecnologia
{
    public class TecnologiaValidator : AbstractValidator<TecnologiaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public TecnologiaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                    .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirTecnologiaValidator : AbstractValidator<TecnologiaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirTecnologiaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((variedade, validateContext) =>
            {
                var conf = _context.variedades.Where(c => c.IdTecnologia == variedade.Id).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Existem Variedades para essa tecnologia");
                }
            });
        }
    }
}