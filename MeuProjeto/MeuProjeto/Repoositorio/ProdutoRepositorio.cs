using MySql.Data.MySqlClient;
using MeuProjeto.Models;
using System.Data;
using System.Security.Cryptography;

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

                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }


        public bool Atualizar(Produto produto)
        {

            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update produto set NomeP=@nomep,DescricaoP=@descricaop,PresoP=@presop,QuantidadeP=@quantidadep" + "where IdPro=@idpro", conexao);
                
                    cmd.Parameters.Add("@idpro", MySqlDbType.Int32).Value = produto.IdPro;
                    cmd.Parameters.Add("@nomep", MySqlDbType.VarChar).Value = produto.NomeP;
                    cmd.Parameters.Add("@descricaop", MySqlDbType.VarChar).Value = produto.DescricaoP;
                    cmd.Parameters.Add("@presop", MySqlDbType.Double).Value = produto.PresoP;
                    cmd.Parameters.Add("@quantidadep", MySqlDbType.Int32).Value = produto.QuantidadeP;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;

                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar Produto: {ex.Message}");
                return false;
            }
        }
    }
}
