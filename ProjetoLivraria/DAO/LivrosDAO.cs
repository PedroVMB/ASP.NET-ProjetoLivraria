using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjetoLivraria.DAO
{
    public class LivrosDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        LivroAutorDAO livroAutorDAO = new LivroAutorDAO();

        public List<Livros> BuscaLivros(decimal? adcIdLivro = null)
        {
            List<Livros> loListLivros = new List<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdLivro != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );

                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar os livros");
                }
            }
            return loListLivros;
        }

        public int InsertLivro(Livros aoNovoLivro)
        {
            if (aoNovoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"INSERT INTO LIV_LIVROS (LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO, LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO) VALUES (@idlivro, @idtipo, @ideditor, @titulo, @preco, @royalty, @resumo, @edicao)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aoNovoLivro.Liv_Id_Livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idtipo", aoNovoLivro.Liv_Id_Tipo_Livro));
                    ioQuery.Parameters.Add(new SqlParameter("@ideditor", aoNovoLivro.Liv_Id_Editor));
                    ioQuery.Parameters.Add(new SqlParameter("@titulo", aoNovoLivro.Liv_Nm_Titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@preco", aoNovoLivro.Liv_Vl_Preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivro.Liv_Pc_Royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aoNovoLivro.Liv_Ds_Resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicao", aoNovoLivro.Liv_Nu_Edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao inserir novo livro");
                }
            }
            return liQtdRegistrosInseridos;
        }



        public int RemoveLivro(Livros aoLivro)
        {
            if (aoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.Liv_Id_Livro));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir livro.");
                }
            }
            return liQtdRegistrosExcluidos;
        }

        public int AtualizaLivro(Livros aolivro)
        {
            if (aolivro == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"UPDATE LIV_LIVROS SET LIV_ID_TIPO_LIVRO = @idCategoria, LIV_ID_EDITOR = @idEditor, 
                                                LIV_NM_TITULO = @titulo,LIV_VL_PRECO = @preco,
                                                LIV_PC_ROYALTY = @royalty,LIV_DS_RESUMO = @resumo,
                                                LIV_NU_EDICAO = @edicao 
                                                WHERE LIV_ID_LIVRO = @idlivro", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aolivro.Liv_Id_Livro));
                    ioQuery.Parameters.Add(new SqlParameter("@titulo", aolivro.Liv_Nm_Titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@preco", aolivro.Liv_Vl_Preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aolivro.Liv_Pc_Royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aolivro.Liv_Ds_Resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicao", aolivro.Liv_Nu_Edicao));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aolivro.Liv_Id_Editor));
                    ioQuery.Parameters.Add(new SqlParameter("@idCategoria", aolivro.Liv_Id_Tipo_Livro));


                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"erro ao atualizar informaçoes do livro. {ex}");
                }
            }
            return liQtdLinhasAtualizadas;
        }

       
       
        public BindingList<Livros> FindLivrosByAutor(Autores autor)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    if (autor != null && autor.aut_id_autor != null)
                    {

                        ioQuery = new SqlCommand("SELECT L.* FROM LIV_LIVROS L INNER JOIN LIA_LIVRO_AUTOR LA ON L.LIV_ID_LIVRO = LA.LIA_ID_LIVRO WHERE LA.LIA_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", autor.aut_id_autor));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                
                                Livros loNovoLivro = new Livros(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                     loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Objeto Autor não fornecido ou sem ID.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por autor. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }

        public BindingList<Livros> FindLivrosByCategoria(decimal idTipoLivro)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE LIV_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", idTipoLivro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );

                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar os livros por tipo");
                }
            }

            return loListLivros;
        }

        public BindingList<Livros> FindLivrosByEditor(decimal idEditor)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE LIV_ID_EDITORES = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", idEditor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );

                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar os livros por editor");
                }
            }

            return loListLivros;
        }

        public Livros FindLivroByAutorAndLivroId(decimal idAutor, decimal idLivro)
        {
            Livros livro = null;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand(@"SELECT LIV.* 
                                      FROM LIV_LIVROS LIV
                                      INNER JOIN LIA_LIVRO_AUTOR LIA ON LIV.LIV_ID_LIVRO = LIA.LIA_ID_LIVRO
                                      WHERE LIA.LIA_ID_AUTOR = @idAutor AND LIV.LIV_ID_LIVRO = @idLivro", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", idAutor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", idLivro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        if (loReader.Read())
                        {
                            livro = new Livros(
                                loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar o livro por autor e ID. Detalhes: " + ex.Message, ex);
                }
            }

            return livro;
        }

       

    }
}
