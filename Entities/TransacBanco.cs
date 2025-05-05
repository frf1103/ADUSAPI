namespace ADUSAPI.Entities
{
    public class TransacBanco
    {
        public string idtransacbanco { get; set; }
        public int idbanco { get; set; }
        public int idtransacao { get; set; }

        public int? idcentrocusto { get; set; }
        public int idcategoria { get; set; }
        public Transacao transacao { get; set; }
        public Banco banco { get; set; }
        public PlanoConta planoConta { get; set; }
        public CentroCusto centroCusto { get; set; }
    }
}