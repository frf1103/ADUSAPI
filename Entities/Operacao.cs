using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Operacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int IdTipoOperacao { get; set; }
        public bool Insumo { get; set; }
        public string? CodigoExterno { get; set; }

        [Precision(18, 2)]
        public decimal? Rendimento { get; set; }

        [Precision(18, 2)]
        public decimal? Consumo { get; set; }

        public string idconta { get; set; }

        [ForeignKey("IdTipoOperacao")]
        public TipoOperacao TipoOperacao { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public ICollection<ModeloParametro>? modelosparametro { get; set; }

        public ICollection<MaquinaParametro>? maquinasparametro { get; set; }

        public ICollection<PlanejamentoOperacao> planejamentoOperacao { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}