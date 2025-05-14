using MySql.Data.MySqlClient;
using MeuProjeto.Models;
using System.Data;

namespace MeuProjeto.Repoositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("inset into produto (NomeP,DescricaoP,PresoP,QuantidadeP) values (@nomep,@descricaop,@presop,@quantidadep)", conexao);

                cmd.Parameters.Add("@nomep", MySqlDbType.Varchar).Value = produto.NomeP;
                cmd.Parameters.Add("@descricaop", MySqlDbType.Varchar).Value = produto.DescricaoP;
                cmd.Parameters.Add("@presop", MySqlDbType.Varchar).Value = produto.PresoP; 
                cmd.Parameters.Add("@quantidadep", MySqlDbType.Varchar).Value = produto.QuantidadeP;
            }

        }

    }
}
