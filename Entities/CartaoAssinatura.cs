namespace ADUSAPI.Entities
{
    public class CartaoAssinatura
    {
        public int Id { get; set; }
        public string IdAssinatura { get; set; }
        public string IdToken { get; set; }
        public string UltimosDigitos { get; set; }
        public bool Ativo { get; set; }
        public string Bandeira { get; set; }

        public Assinatura Assinatura { get; set; }
    }
}