using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class CategoriaDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Categorias> BuscaCategorias(decimal? adcIdTipoLivro = null)
        {
            BindingList<Categorias> loListTipoLivros = new BindingList<Categorias>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdTipoLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", adcIdTipoLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Categorias loNovoTipoLivro = new Categorias(
                                loReader.GetDecimal(0),
                                loReader.GetString(1)  
                            );

                            loListTipoLivros.Add(loNovoTipoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar as categorias livros");
                }
            }
            return loListTipoLivros;
        }


        public int InsertCategoria(Categorias aoNovoTipoLivro)
        {
            if (aoNovoTipoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"INSERT INTO TIL_TIPO_LIVRO(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO)
                VALUES(@idTipoLivro, @descricaoTipoLivro)", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.TIL_ID_TIPO_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoNovoTipoLivro.TIL_DS_DESCRICAO));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar cadastrar novo Tipo livro: {0}", ex);
                }
            }
            return liQtdRegistrosInseridos;
        }


        public int RemoveCategoria(Categorias aoTipoLivro)
        {
            if (aoTipoLivro == null)
                throw new NullReferenceException();

            int liQtdRegistrosExcluidos = 0;

            
            if (CategoriaPossuiLivrosAssociados(aoTipoLivro.TIL_ID_TIPO_LIVRO))
            {
                throw new Exception("Não é possível excluir o tipo de livro pois existem livros associados a ele.");
            }

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoTipoLivro.TIL_ID_TIPO_LIVRO));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();


                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir Tipo livro.");
                }
            }

            return liQtdRegistrosExcluidos;
        }

        private bool CategoriaPossuiLivrosAssociados(decimal idTipoLivro)
        {
            using (SqlConnection ioConexaoLocal = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexaoLocal.Open();
                    ioQuery = new SqlCommand("SELECT COUNT(*) FROM LIV_LIVROS WHERE LIV_ID_TIPO_LIVRO = @idTipoLivro", ioConexaoLocal);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", idTipoLivro));

                    int count = (int)ioQuery.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao verificar se o tipo de livro possui livros associados. Detalhes: {ex.Message}');</script>");
                    return false; 
                }
            }
        }


        public int AtualizaCategoria(Categorias aoTipoLivro)
        {
            if (aoTipoLivro == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"UPDATE TIL_TIPO_LIVRO SET TIL_DS_DESCRICAO = @descricaoTipoLivro WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro ", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoTipoLivro.TIL_ID_TIPO_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoTipoLivro.TIL_DS_DESCRICAO));
                    

                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do Tipo livro.");
                }
            }
            return liQtdLinhasAtualizadas;
        }

        public string BuscarDescricaoCategoria(decimal idCategoria)
        {
            string descricaoCategoria = string.Empty;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT TIL_DS_DESCRICAO FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idCategoria", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idCategoria", idCategoria));

                    using (SqlDataReader reader = ioQuery.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            descricaoCategoria = reader["TIL_DS_DESCRICAO"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar descrição da categoria. Detalhes: {ex.Message}');</script>");
                }
            }

            return descricaoCategoria;
        }

        public decimal BuscaIdCategoriaByNome(string nomeCategoria)
        {
            decimal idTipoLivro = 0; 

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT TIL_ID_TIPO_LIVRO FROM TIL_TIPO_LIVRO WHERE TIL_DS_DESCRICAO = @nomeCategoria", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@nomeCategoria", nomeCategoria));

                    using (SqlDataReader reader = ioQuery.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idTipoLivro = reader.GetDecimal(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar ID da categoria. Detalhes: {ex.Message}');</script>");
                }
            }

            return idTipoLivro;
        }

    }
}