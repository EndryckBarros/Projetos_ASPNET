using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeilaoWeb.Models
{
    public class Lance
    {
        public int LanceID { get; set; }
        public int PessoaID { get; set; }
        public int ProdutoID { get; set; }
        public int Valor { get; set; }

        public virtual Produto _Produto { get; set; }
        public virtual Pessoa _Pessoa { get; set; }
    }
}