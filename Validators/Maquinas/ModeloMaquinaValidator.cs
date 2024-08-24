using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Maquinas
{
    public class ModeloMaquinaValidator : AbstractValidator<ModeloMaquinaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ModeloMaquinaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirModeloMaquinaValidator : AbstractValidator<ModeloMaquinaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirModeloMaquinaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((modelomaq, validateContext) =>
            {

                var princ = context.maquinas.FirstOrDefault(c => c.IdModeloMaquina == modelomaq.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem maquinas vinculados ao Modelo");
                }
                var par = context.modelosparametros.FirstOrDefault(c => c.IdModeloMaquina == modelomaq.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem parâmetros vinculados ao Modelo");
                }
                var princ2 = context.maquinasplanejadas.FirstOrDefault(c => c.IdModeloMaquina == modelomaq.Id);
                if (princ2 != null)
                {
                    validateContext.AddFailure("Modelo com planejamento");
                }


            });
        }

    }
}
