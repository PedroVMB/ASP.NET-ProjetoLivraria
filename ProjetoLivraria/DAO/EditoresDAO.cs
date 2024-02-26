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
    public class EditoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Editores> BuscaEditores(decimal? adcIdEditor = null)
        {
            BindingList<Editores> loListEditores = new BindingList<Editores>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if(adcIdEditor != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT * FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", adcIdEditor));
                    }else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Editores loNovoEditor = new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));

                            loListEditores.Add(loNovoEditor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {

                    throw new Exception("Erro ao tentar buscar o(s) editor(s)");
                }
            }
            return loListEditores;
        }

        public int InsertEditor(Editores aoNovoEditor)
        {
            {
                if(aoNovoEditor == null)
                    throw new NullReferenceException();
                int liQtdRegistrosInseridos = 0;
                using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    try
                    {
                        ioConexao.Open();
                        ioQuery = new SqlCommand(@"INSERT INTO EDI_EDITORES(EDI_ID_EDITOR, EDI_NM_EDITOR, EDI_DS_EMAIL, EDI_DS_URL) 
                        VALUES (@idEditor, @nomeEditor, @emailEditor, @urlEditor)", ioConexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.EDI_ID_EDITOR));
                        ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoNovoEditor.EDI_NM_EDITOR));
                        ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoNovoEditor.EDI_DS_EMAIL));
                        ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoNovoEditor.EDI_DS_URL));

                        liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                    }
                    catch
                    {

                        throw new Exception("Erro ao tentar cadastrar novo editor.");
                    }
                }
                return liQtdRegistrosInseridos;
            }
        }

        public int AtualizaEditor(Editores aoEditor)
        {
            if (aoEditor == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"UPDATE EDI_EDITORES SET EDI_NM_EDITOR = @nomeEditor, EDI_DS_EMAIL = @emailEditor,
                   EDI_DS_URL = @urlEditor WHERE EDI_ID_EDITOR = @idEditor", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.EDI_ID_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoEditor.EDI_NM_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoEditor.EDI_DS_EMAIL));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoEditor.EDI_DS_URL));
                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do editor.");
                }
            }
            return liQtdLinhasAtualizadas;
        }

        public int RemoveEditor(Editores aoEditor)
        {
            if (aoEditor == null)
                throw new NullReferenceException();

            int liQtdRegistrosExcluidos = 0;

            if(LivrosAssociadosAoEditor(aoEditor.EDI_ID_EDITOR))
            {
                throw new Exception("Não é possível excluir o editor pois existem livros associados a ele.");
            }

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.EDI_ID_EDITOR));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                   

                }
                catch (Exception)
                {

                    throw new Exception("Erro ao tentar excluir o editor.");
                }
            }
            return liQtdRegistrosExcluidos;
        }

        public bool LivrosAssociadosAoEditor(decimal idEditor)
        {
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT COUNT(*) FROM LIV_LIVROS WHERE LIV_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", idEditor));

                    int count = (int)ioQuery.ExecuteScalar();

                    return count > 0;
                }
                catch(Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao verificar se o tipo de livro possui livros associados. Detalhes: {ex.Message}');</script>");
                    return false;
                }
                
            }
        }

        public string BuscarNomeEditor(decimal idEditor)
        {
            string nomeEditor = string.Empty;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT EDI_NM_EDITOR FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", idEditor));

                    using (SqlDataReader reader = ioQuery.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nomeEditor = reader["EDI_NM_EDITOR"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar nome do editor. Detalhes: {ex.Message}');</script>");
                }
            }

            return nomeEditor;
        }

        internal decimal BuscaIdEditorByNome(string nomeEditor)
        {
            decimal idEditor = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(@"SELECT EDI_ID_EDITOR FROM EDI_EDITORES WHERE EDI_NM_EDITOR = @nomeEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", nomeEditor));

                    using (SqlDataReader reader = ioQuery.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idEditor = reader.GetDecimal(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar ID do editor. Detalhes: {ex.Message}');</script>");
                }
            }

            return idEditor;
        }
    }
}