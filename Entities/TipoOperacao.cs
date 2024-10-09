using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class TipoOperacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public bool? plantio { get; set; } = false;
        public ICollection<Operacao> operacoes { get; set; }
    }
}