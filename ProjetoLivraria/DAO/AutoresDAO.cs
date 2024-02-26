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
    public class AutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Autores> BuscaAutores(decimal? adcIdAutor = null)
        {
            BindingList<Autores> loListAutores = new BindingList<Autores>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if(adcIdAutor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdAutor));
                    }else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutor = new Autores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));

                            loListAutores.Add(loNovoAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) autor(s)");
                }
            }
            return loListAutores;
        }

        public int InsertAutor(Autores aoNovoAutor)
        {
            {
              
                if (aoNovoAutor == null)
                    throw new NullReferenceException();
                int liQtdRegistrosInseridos = 0;
                using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    try
                    {
                        ioConexao.Open();
                        ioQuery = new SqlCommand(@"INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL)
                       VALUES(@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)", ioConexao);
                       
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor));
                        ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoNovoAutor.aut_nm_nome));
                        ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoNovoAutor.aut_nm_sobrenome));
                        ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoNovoAutor.aut_ds_email));
                        
                        liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new Exception("Erro ao tentar cadastrar novo autor.");
                    }
                }
                return liQtdRegistrosInseridos;
            }
        }

        public int RemoveAutor(Autores aoAutor)
        {
            if (aoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.aut_id_autor));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir autor.");
                }
            }
            return liQtdRegistrosExcluidos;
        }

        public int AtualizaAutor(Autores aoAutor)
        {
            if (aoAutor == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"UPDATE AUT_AUTORES SET AUT_NM_NOME = @nomeAutor, AUT_NM_SOBRENOME = @sobrenomeAutor,
                   AUT_DS_EMAIL = @emailAutor WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                   
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoAutor.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoAutor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoAutor.aut_ds_email));
                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do autor.");
                }
            }
            return liQtdLinhasAtualizadas;
        }

        public decimal BuscaIdAutorByNome(string nomeAutor)
        {
            decimal idAutor = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT AUT_ID_AUTOR FROM AUT_AUTORES WHERE AUT_NM_NOME = @nomeAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", nomeAutor));

                    using (SqlDataReader reader = ioQuery.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idAutor = reader.GetDecimal(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar ID do Autor. Detalhes: {ex.Message}');</script>");
                }
            }

            return idAutor;
        }

    }
}