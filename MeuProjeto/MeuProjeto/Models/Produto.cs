namespace MeuProjeto.Models
{
    public class Produto
    {
        public int IdPro { get; set; }
        public string NomeP { get; set; }
        public string DescricaoP { get; set; }
        public decimal PresoP { get; set; }
        public int QuantidadeP {  get; set; }
        public List<Produto>? Produtolist { get; set; }

    }
}
