namespace ADUSAPI.Entities
{
    public class ParametrosGuru
    {
        public int id { get; set; }
        public string token { get; set; }
        public DateTime ultdata { get; set; }
        public string urlsub { get; set; }
        public string urltransac { get; set; }
        public int? idtransacao { get; set; }
        public int? idcategoria { get; set; }
        public string? idconta { get; set; }
        public string? idparceiro { get; set; }
        public int? idccusto { get; set; }

        public int? idcategoriataxa { get; set; }
        public int? idcategoriaant { get; set; }
        public int? idcategoriacomiss { get; set; }
        public virtual Transacao Transacao { get; set; }

        public virtual CentroCusto CentroCusto { get; set; }
        public virtual ContaCorrente ContaCorrente { get; set; }
        public virtual PlanoConta Categoria { get; set; }
        public virtual Parceiro parceiro { get; set; }
        public virtual PlanoConta? catergoriataxa { get; set; }
        public virtual PlanoConta? catergoriaant { get; set; }
        public virtual PlanoConta? catergoriacomiss { get; set; }
        public int? idtransacaotaxa { get; set; }
        public int? idtransacaoant { get; set; }
        public int? idtransacaocomiss { get; set; }
        public virtual Transacao TransacaoTaxa { get; set; }
        public virtual Transacao TransacaoAnt { get; set; }
        public virtual Transacao TransacaoComiss { get; set; }
    }
}