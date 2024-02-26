using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livros
    {
      
        public decimal Liv_Id_Livro { get; set; }
        public decimal Liv_Id_Tipo_Livro { get; set; }
        public decimal Liv_Id_Editor { get; set; }
        public string Liv_Nm_Titulo { get; set; }
        public decimal Liv_Vl_Preco { get; set; }
        public decimal Liv_Pc_Royalty { get; set; }
        public string Liv_Ds_Resumo { get; set; }
        public int Liv_Nu_Edicao { get; set; }

        public Livros(decimal adcIdLivro, decimal adcIdTipoLivro, decimal adcIdEditor, string adcTituloLivro, decimal adcPrecoLivro, decimal adcRoyaltyLivro, string adcResumoLivro, int adcNumeroEdicaoLivro)
        {
            this.Liv_Id_Livro = adcIdLivro;
            this.Liv_Id_Tipo_Livro = adcIdTipoLivro;
            this.Liv_Id_Editor = adcIdEditor;
            this.Liv_Nm_Titulo = adcTituloLivro;
            this.Liv_Vl_Preco = adcPrecoLivro;
            this.Liv_Pc_Royalty = adcRoyaltyLivro;
            this.Liv_Ds_Resumo = adcResumoLivro;
            this.Liv_Nu_Edicao = adcNumeroEdicaoLivro;
        }

        public Livros()
        {
        }
    }
}