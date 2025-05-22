using System.ComponentModel.DataAnnotations;

namespace ADUSAPI.Entities
{
    public class Convite
    {
        [Key]
        public string IdConvite { get; set; }

        [Required]
        public string Fone { get; set; }

        public string? Email { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public DateTime DataExpiracao { get; set; }

        [Required]
        public string IdAfiliado { get; set; }

        [Required]
        public int IdPlataforma { get; set; }

        [Required]
        public int Status { get; set; }

        public Parceiro afiliado { get; set; }
        public Assinatura? assinatura { get; set; }
        public string? idassinatura { get; set; }

        [MaxLength(20)]
        public string? idformapgto { get; set; }

        public string? Titular { get; set; }
    }
}