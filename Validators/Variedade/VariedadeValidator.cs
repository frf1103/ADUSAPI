using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Safra;
using FarmPlannerAPICore.Models.Variedade;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Variedade{
    
        public class VariedadeValidator : AbstractValidator<VariedadeViewModel>
        {
            private readonly FarmPlannerContext _context;
            public VariedadeValidator(FarmPlannerContext context)
            {
                _context = context;

                RuleFor(c => c.Descricao)
                    .NotEmpty().WithMessage("É necessário informar a Descricao.")
                    .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
            RuleFor(c => c.Ciclo)
                .NotEmpty().WithMessage("É necessário informar o ciclo.");
            RuleFor(c => c.IdCultura)
                .NotEmpty().WithMessage("É necessário informar a Cultura.");
            RuleFor(c => c.IdTecnologia)
                .NotEmpty().WithMessage("É necessário informar a Tecnologia.");


        }

        public class ExcluirVariedadeValidator : AbstractValidator<VariedadeViewModel>
        {
            private readonly FarmPlannerContext _context;
            public ExcluirVariedadeValidator(FarmPlannerContext context)
            {
                _context = context;


                RuleFor(c => c).Custom((variedade, validateContext) =>
                {
                    var conf = _context.configareas.Where(c => c.IdVariedade == variedade.Id).FirstOrDefault();
                    if (conf != null)
                    {
                        validateContext.AddFailure("Existem configuraçoes para essa variedade");
                    }
                });

            }


        }


    }

}
