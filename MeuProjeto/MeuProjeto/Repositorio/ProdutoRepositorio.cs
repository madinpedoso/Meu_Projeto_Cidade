using MySql.Data.MySqlClient;
using MeuProjeto.Models;
using System.Data;
using System.Security.Cryptography;

namespace MeuProjeto.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into produto (NomeP,DescricaoP,PresoP,QuantidadeP) values (@nomep,@descricaop,@presop,@quantidadep)", conexao);

                cmd.Parameters.Add("@nomep", MySqlDbType.VarChar).Value = produto.NomeP;
                cmd.Parameters.Add("@descricaop", MySqlDbType.VarChar).Value = produto.DescricaoP;
                cmd.Parameters.Add("@presop", MySqlDbType.Decimal).Value = produto.PresoP; 
                cmd.Parameters.Add("@quantidadep", MySqlDbType.Int32).Value = produto.QuantidadeP;

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
                    MySqlCommand cmd = new MySqlCommand(
    "UPDATE Produto SET NomeP=@nomep, DescricaoP=@descricaop, PresoP=@presop, QuantidadeP=@quantidadep " +
    "WHERE IdPro=@idpro", conexao);

                    cmd.Parameters.Add("@idpro", MySqlDbType.Int32).Value = produto.IdPro;
                    cmd.Parameters.Add("@nomep", MySqlDbType.VarChar).Value = produto.NomeP;
                    cmd.Parameters.Add("@descricaop", MySqlDbType.VarChar).Value = produto.DescricaoP;
                    cmd.Parameters.Add("@presop", MySqlDbType.Decimal).Value = produto.PresoP;
                    cmd.Parameters.Add("@quantidadep", MySqlDbType.Int32).Value = produto.QuantidadeP;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar Produto: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProduto()
        {
            List<Produto> Produtolist = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produto", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();


                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                        new Produto
                        {
                            IdPro = Convert.ToInt32(dr["IdPro"]),
                            NomeP = ((string)dr["NomeP"]),
                            DescricaoP = ((string)dr["DescricaoP"]),
                            PresoP = (decimal)(dr["Presop"]),
                            QuantidadeP = Convert.ToInt32(dr["QuantidadeP"]),
                        });

                   
                }
                return Produtolist;
            }
        }


        public Produto ObterProduto(int IdPro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * from produto where IdPro=@idpro ", conexao);
                cmd.Parameters.AddWithValue("@idpro", IdPro);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    produto.IdPro = Convert.ToInt32(dr["IdPro"]);
                    produto.NomeP = ((string)dr["NomeP"]);
                    produto.DescricaoP = ((string)dr["DescricaoP"]);
                    produto.PresoP = (decimal)(dr["Presop"]);
                    produto.QuantidadeP = Convert.ToInt32(dr["QuantidadeP"]);
                }
                return produto;
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from Produto where IdPro=@idpro", conexao);

                cmd.Parameters.AddWithValue("@idpro", Id);

                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}

