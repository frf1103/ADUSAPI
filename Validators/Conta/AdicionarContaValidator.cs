using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Conta;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using FarmPlannerAPI.Shared;

namespace FarmPlannerAPI.Validators.Conta
{
    public class AdicionarContaValidator : AbstractValidator<AdicionarUsuarioConta>
    {
        private readonly FarmPlannerContext _context;

        public AdicionarContaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar o Nome.")
                .MaximumLength(100).WithMessage("O nome da Conta deve ter no máximo 100 caracteres");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("É necessário informar o Email do titular da conta")
                .MaximumLength(100).WithMessage("O Email deve ter no máximo 100 caracteres")
                .Custom((Email, validateContext) =>
                {
                    var conta = context.contas.Where(c => c.Email == Email).FirstOrDefault();
                    if (conta != null)
                    {
                        validateContext.AddFailure("Email já existente para outra conta");
                    }
                });
            RuleFor(c => c.CPF).Custom((CPF, validateContext) =>
            {
                if (FBSLIb.StringLib.ValidaRegistro(0, CPF) == false)
                {
                    validateContext.AddFailure("CPF Inválido");
                }
                var conta = context.contas.Where(c => c.CPF == CPF).FirstOrDefault();
                if (conta != null)
                {
                    validateContext.AddFailure("CPF já existente para outra conta");
                }
            });
        }

        /*        public bool ValidaCPF(string cpf)
                {
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
                }*/
    }
}