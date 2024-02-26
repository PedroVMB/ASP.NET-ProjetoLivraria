using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Categorias
    {
        public decimal TIL_ID_TIPO_LIVRO { get; set; }
        public string TIL_DS_DESCRICAO { get; set; }

        public Categorias(decimal adcIdTipoLivro, string adcDescricaoTipoLivro)
        {
            this.TIL_ID_TIPO_LIVRO = adcIdTipoLivro;
            this.TIL_DS_DESCRICAO = adcDescricaoTipoLivro;
        }
        
    }
}