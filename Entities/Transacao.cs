using ADUSAPICore.Models.Enum;

namespace ADUSAPI.Entities
{
    public class Transacao
    {
        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public string Sinal { get; set; } = string.Empty; // Deve ser "D" ou "C"

        public TipoContra Contrapartida { get; set; }
        public ICollection<MovimentoCaixa>? movs { get; set; }
        public ICollection<ParametrosGuru>? parametros { get; set; }
        public ICollection<ParametrosGuru>? parametrostaxa { get; set; }
        public ICollection<ParametrosGuru>? parametrosant { get; set; }
        public ICollection<ParametrosGuru>? parametroscomiss { get; set; }
        public ICollection<TransacBanco>? transacs { get; set; }
    }
}