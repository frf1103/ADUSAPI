using ADUSAPI.Context;
using ADUSAPICore.Models.Parceiro;
using FluentValidation;

namespace ADUSAPI.Validators.Parceiro
{
    public class ParceiroValidator : AbstractValidator<ParceiroViewModel>
    {
        private readonly ADUSContext _context;

        public ParceiroValidator(ADUSContext context)
        {
            _context = context;

            RuleFor(c => c.RazaoSocial)
                .NotEmpty().WithMessage("É necessário informar o Nome.")
                .MaximumLength(100).WithMessage("O nome do Parceiro deve ter no máximo 100 caracteres");

            RuleFor(c => c.Fantasia)
                .NotEmpty().WithMessage("É necessário informar a Fantasia do Parceiro")
                .MaximumLength(50).WithMessage("A Fantasia deve ter no máximo 50 caracteres");
            RuleFor(c => c).Custom((parceiro, validateContext) =>
            {
                var tipo = (int)parceiro.TipodePessoa;
                var reg = parceiro.Registro;
                if (ValidaRegistro(tipo, reg) == false)
                {
                    validateContext.AddFailure("Registro Inválido");
                }
                var conta = context.parceiros.Where(c => c.Registro == FBSLIb.StringLib.Somentenumero(parceiro.Registro.ToString()) && c.uid != parceiro.Id).FirstOrDefault();
                if (conta != null)
                {
                    validateContext.AddFailure("CPF/CNPJ já existente para outra organização");
                }
            });
        }

        public bool ValidaRegistro(int tipo, string registro)
        {
            if (tipo == 0)
            {
                string cpf = registro;
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
            else
            {
                string cnpj = registro;
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj.Length != 14)
                    return false;
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cnpj.EndsWith(digito);
            }
        }
    }

    public class ExcluirParceiroValidator : AbstractValidator<ParceiroViewModel>
    {
        private readonly ADUSContext _context;

        public ExcluirParceiroValidator(ADUSContext context)
        {
            _context = context;

            /*
            RuleFor(c => c).Custom((parceiro, validateContext) =>
            {
                var prod = context.produtos.Where(c => c.IdFabricante == parceiro.Id).FirstOrDefault();
                if (prod != null)
                {
                    validateContext.AddFailure("Esse parceiro é fabricante de alguns produtos");
                }
            });
            */
        }
    }
}