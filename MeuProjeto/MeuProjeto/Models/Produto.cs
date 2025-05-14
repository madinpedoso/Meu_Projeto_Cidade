namespace MeuProjeto.Models
{
    public class Produto
    {
        public int IdPro { get; set; }
        public string NomeP { get; set; }
        public string DescricaoP { get; set; }
        public double PresoP { get; set; }
        public int QuantidadeP {  get; set; }
        public List<Produto>? ListaProduto{ get; set; }

    }
}
