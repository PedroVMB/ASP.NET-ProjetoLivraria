using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    public class LivroAutor
    {
        public decimal LIA_ID_AUTOR { get; set; }
        public decimal LIA_ID_LIVRO { get; set; }
        public decimal LIA_PC_ROYALTY { get; set; }

        public LivroAutor(decimal adcIdAutor, decimal adcIdLivro, decimal adcPcRoyalty)
        {
            this.LIA_ID_AUTOR = adcIdAutor;
            this.LIA_ID_LIVRO = adcIdLivro;
            this.LIA_PC_ROYALTY = adcPcRoyalty;
        }
    }
}