using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avaliacao_wesleyandrade_microondas.Models
{
    public class Programa
    {

        public String nome { get; set; }
        public String instrucoes { get; set; }
        public int tempo { get; set; }
        public int potencia { get; set; }
        public char caractere { get; set; }

        public Boolean original{get;set;} // para idenficar quais programas são originais de fábrica (Não podem ser excluídos)

    }
}