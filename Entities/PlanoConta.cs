﻿namespace ADUSAPI.Entities
{
    public class PlanoConta
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int? IdMae { get; set; }

        public string Sinal { get; set; }

        // Autorelacionamento
        public PlanoConta Mae { get; set; }

        public ICollection<PlanoConta>? Filhos { get; set; }
        public ICollection<MovimentoCaixa>? movimentoCaixas { get; set; }
        public ICollection<ParametrosGuru>? parametros { get; set; }
        public ICollection<TransacBanco>? transacs { get; set; }
        public ICollection<ParametrosGuru>? parametrosTaxa { get; set; }
        public ICollection<ParametrosGuru>? parametrosAnt { get; set; }
        public ICollection<ParametrosGuru>? parametroscomiss { get; set; }
    }
}