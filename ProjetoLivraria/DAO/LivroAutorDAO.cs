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
    public class LivroAutorDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        public int InsereLivroAutor(LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"INSERT INTO LIA_LIVRO_AUTOR (LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY) VALUES (@idautor, @idlivro, @royalty)", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idautor", aoNovoLivroAutor.LIA_ID_AUTOR));
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aoNovoLivroAutor.LIA_ID_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivroAutor.LIA_PC_ROYALTY));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"erro ao inserir novo livro e autor. Detalhes: {ex}");
                }
            }
            return liQtdRegistrosInseridos;

        }

        public BindingList<Autores> BuscaNomeAutorPeloIdDoLivroAssociado(decimal idLivro)
        {
            BindingList<Autores> loListAutoresAssociadosALivros = new BindingList<Autores>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT *
                                                FROM AUT_AUTORES autores
                                                INNER JOIN LIA_LIVRO_AUTOR livroAutor 
                                                ON autores.AUT_ID_AUTOR = livroAutor.LIA_ID_AUTOR
                                                INNER JOIN LIV_LIVROS livros 
                                                ON livroAutor.LIA_ID_LIVRO = livros.LIV_ID_LIVRO
                                                WHERE livros.LIV_ID_LIVRO = @idLivro;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", idLivro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutor = new Autores(
                                loReader.GetDecimal(0),
                                loReader.GetString(1),
                                loReader.GetString(2),
                                loReader.GetString(3)
                            );

                            loListAutoresAssociadosALivros.Add(loNovoAutor);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Erro ao tentar buscar o Autor pelo livro associado a ele. Detalhes: " + ex.Message, ex);
                }
            }
            return loListAutoresAssociadosALivros;
        }

        public int AtualizaIdAutorPeloIdDoLivro(LivroAutor aoLivroAutor)
        {
            if (aoLivroAutor == null)
                throw new NullReferenceException();

            int liQtdLinhasAtualizadas = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"UPDATE LIA_LIVRO_AUTOR 
                                      SET LIA_ID_AUTOR = @idAutor
                                      WHERE LIA_ID_LIVRO = @idLivro", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoLivroAutor.LIA_ID_AUTOR));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivroAutor.LIA_ID_LIVRO));

                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar ID do autor. Detalhes: " + ex.Message, ex);
                }
            }

            return liQtdLinhasAtualizadas;
        }



    }
}