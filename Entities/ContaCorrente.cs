namespace ADUSAPI.Entities
{
    public class ContaCorrente
    {
        public string Id { get; set; } = null!;

        public string Descricao { get; set; } = string.Empty;

        public string Agencia { get; set; } = string.Empty;

        public string ContaBanco { get; set; } = string.Empty;

        public string Titular { get; set; } = string.Empty;

        public int BancoId { get; set; }

        public Banco Banco { get; set; } = null!;
    }
}