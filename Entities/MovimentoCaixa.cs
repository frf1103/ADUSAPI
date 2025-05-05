using ADUSAPI.Entities;
using System;

namespace ADUSAPI.Entities
{
    public class MovimentoCaixa
    {
        public int Id { get; set; }

        public int IdTransacao { get; set; }
        public int IdCentroCusto { get; set; }
        public string IdContaCorrente { get; set; }
        public int IdCategoria { get; set; } // Refere-se a PlanoConta

        public string Sinal { get; set; } = string.Empty; // D ou C
        public string? Observacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataMov { get; set; }

        public string idparceiro { get; set; }

        // Navegações
        public virtual Transacao Transacao { get; set; }

        public virtual CentroCusto CentroCusto { get; set; }
        public virtual ContaCorrente ContaCorrente { get; set; }
        public virtual PlanoConta Categoria { get; set; }
        public virtual Parceiro parceiro { get; set; }
        public string? idmovbanco { get; set; }
    }
}