using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class AnoAgricola
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string? CodigoExterno { get; set; }
        public ICollection<Safra> Safras { get; set; }
        public ICollection<Talhao> Talhoes { get; set; }

        public int IdOrganizacao { get; set; }

        [ForeignKey("IdOrganizacao,idconta")]
        public Organizacao organizacao { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public ICollection<PreferUsu> preferUsus { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}