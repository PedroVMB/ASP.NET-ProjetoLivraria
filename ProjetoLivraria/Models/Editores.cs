using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Editores
    {
        public decimal EDI_ID_EDITOR { get; set; }
        public string EDI_NM_EDITOR { get; set; }
        public string EDI_DS_EMAIL { get; set; }
        public string EDI_DS_URL { get; set; }

        public Editores(decimal adcIdEditor, string adcNomeEditor, string adcEmailEditor, string adcUrlEditor)
        {
            this.EDI_ID_EDITOR = adcIdEditor;
            this.EDI_NM_EDITOR = adcNomeEditor;
            this.EDI_DS_EMAIL = adcEmailEditor;
            this.EDI_DS_URL = adcUrlEditor;
        }
    }
}