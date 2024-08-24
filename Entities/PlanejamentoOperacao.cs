using FarmPlannerAPI.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class PlanejamentoOperacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdConfigArea { get; set; }
        public DateTime DataPrevista { get; set; }
        public int IdOperacao { get; set; }

        public bool Plantio { get; set; }
        public int DAE { get; set; }

        //    public int IdOrcamento { get; set; }
        [Precision(18, 2)]
        public decimal Area { get; set; }

        [Precision(18, 2)]
        public decimal QHorasEstimadas { get; set; }

        [Precision(18, 2)]
        public decimal QCombustivelEstimado { get; set; }

        [Precision(18, 2)]
        public int Status { get; set; }

        [Precision(18, 2)]
        public decimal CustoOperacao { get; set; }

        [ForeignKey("IdConfigArea,idconta")]
        public ConfigArea configArea { get; set; }

        //  [ForeignKey("IdOrcamento")]
        //  public OrcamentoProduto orcamentoProduto { get; set; }

        [ForeignKey("IdOperacao,idconta")]
        public Operacao operacao { get; set; }

        public ICollection<ProdutoPlanejado> produtosplanejados { get; set; }
        public ICollection<MaquinaPlanejada> maquinasplanejada { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}