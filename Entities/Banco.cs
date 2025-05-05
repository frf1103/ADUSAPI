using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADUSAPI.Entities
{
    public class Banco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(4)]
        public string Codigo { get; set; }

        public ICollection<ContaCorrente> ContasCorrentes { get; set; } = new List<ContaCorrente>();

        public ICollection<TransacBanco>? transacs { get; set; }
    }
}