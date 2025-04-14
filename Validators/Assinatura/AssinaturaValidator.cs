using ADUSAPI.Context;
using ADUSAPICore.Models.Assinatura;
using FluentValidation;

namespace ADUSAPI.Validators.Assinatura
{
    public class AssinaturaValidator : AbstractValidator<AssinaturaViewModel>
    {
        private readonly ADUSContext _context;

        public AssinaturaValidator(ADUSContext context)
        {
            _context = context;
            /*
                        RuleFor(c => c.RazaoSocial)
                            .NotEmpty().WithMessage("É necessário informar o Nome.")
                            .MaximumLength(100).WithMessage("O nome do Assinatura deve ter no máximo 100 caracteres");

                        RuleFor(c => c.Fantasia)
                            .NotEmpty().WithMessage("É necessário informar a Fantasia do Assinatura")
                            .MaximumLength(50).WithMessage("A Fantasia deve ter no máximo 50 caracteres");
                        RuleFor(c => c).Custom((Assinatura, validateContext) =>
                        {
                            var tipo = (int)Assinatura.TipodePessoa;
                            var reg = Assinatura.Registro;
                            if (ValidaRegistro(tipo, reg) == false)
                            {
                                validateContext.AddFailure("Registro Inválido");
                            }
                            var conta = context.Assinaturas.Where(c => c.Registro == FBSLIb.StringLib.Somentenumero(Assinatura.Registro.ToString()) && c.uid != Assinatura.Id).FirstOrDefault();
                            if (conta != null)
                            {
                                validateContext.AddFailure("CPF/CNPJ já existente para outra organização");
                            }
                        });
*/
        }
    }
}