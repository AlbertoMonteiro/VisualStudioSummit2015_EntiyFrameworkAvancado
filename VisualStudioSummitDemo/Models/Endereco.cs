using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualStudioSummitDemo.Models
{
    public class Endereco
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        public int Cep { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1} - {2}", Rua, Numero, Bairro);
        }
    }
}
