using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class Maquina
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int Ano { get; set; }
        public string? CodigoExterno { get; set; }
        public int IdModeloMaquina { get; set; }
        public int idorganizacao { get; set; }

        [ForeignKey("IdModeloMaquina")]
        public ModeloMaquina modeloMaquina { get; set; }

        public ICollection<MaquinaParametro> maquinaParametros { get; set; }
        public ICollection<MaquinaPlanejada> maquinasplanejada { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        [ForeignKey("idorganizacao")]
        public Organizacao organizacao { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}