using FarmPlannerAPI.Entities.Enum.FarmPlannerAPI.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class Talhao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Descricao { get; set; }

        [Precision(18, 2)]
        public decimal AreaProdutiva { get; set; }

        public TipodeArea TipoArea { get; set; }
        public string? CodigoExterno { get; set; }
        public int IdFazenda { get; set; }
        public int IdAnoAgricola { get; set; }

        [ForeignKey("IdFazenda,idconta")]
        public Fazenda fazenda { get; set; }

        [ForeignKey("IdAnoAgricola,idconta")]
        public AnoAgricola anoagricola { get; set; }

        public ICollection<ConfigArea> configAreas { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}