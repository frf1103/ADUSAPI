using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Safra
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string? CodigoExterno { get; set; }
        public bool Abertura { get; set; }
        public bool Reforma { get; set; }
        public int? IdCultura { get; set; } = null;
        public int IdAnoAgricola { get; set; }
        public AnoAgricola anoAgricola { get; set; }
        public Cultura cultura { get; set; }
        public ICollection<ConfigArea> configAreas { get; set; }
        public ICollection<OrcamentoProduto> OrcamentoProduto { get; set; }
        public ICollection<Comercializacao> comercializacao { get; set; }
        public ICollection<PlanejamentoCompra> planejamentoCompras { get; set; }
        public ICollection<OrcamentoCustoIndireto> orcamentoCustoIndiretos { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}